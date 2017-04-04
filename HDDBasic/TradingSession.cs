using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using fxcore2;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using NLog;

namespace HDD
{
    enum AccountType
    {
        UK = 0,
        US = 1,
        Master = 2
    };
    public class TradingSession : IO2GSessionStatus, IO2GTableListener, IO2GResponseListener
    {
        private readonly static object IDlock = new object();                                // Unique sessionID
        private O2GSession mSession;
        private List<String> mAccounts;                              // Store account#s associated with this login/Session
        private O2GAccountsTable accountsTbl;
        private O2GTableManager tableManager;
        private O2GOffersTable offersTbl;
        private O2GOrdersTable ordersTbl;
        private O2GClosedTradesTable closedTradesTbl;
        private O2GTradesTable tradesTbl;
        //private O2GSummaryTable summaryTbl;
        //private O2GMessagesTable messageTbl;
        private string serverName;
        private O2GSessionStatusCode connectionStatus;
        private string error = "";
        private Logger logger = LogManager.GetCurrentClassLogger();

        public TradingSession()
        {
            logger.Debug("Creating Trading Session");
            mSession = O2GTransport.createSession();                //Create new Session
            mSession.useTableManager(O2GTableManagerMode.Yes, null);//using fxconnect's built in tables
            mAccounts = new List<string>();
            mSession.subscribeResponse(this);                       //Add SessionListener
            mSession.subscribeSessionStatus(this);                  //Add ResponseListener  
            O2GTransport.ApplicationID = "FXCM Historical Data Downloader Basic";
        }

        #region properties
        public O2GSession Session
        {
            get { return mSession; }
            set { mSession = value; }
        }
        public O2GSessionStatusCode ConnectionStatus
        {
            get { return connectionStatus; }
        }
        public O2GOffersTable OffersTable
        {
            get { return offersTbl; }
        }

        public string Error
        {
            get
            {
                return error;
            }

            set
            {
                error = value;
            }
        }
        #endregion

        #region Login/Logout
        public void login(string username, string password, string url, string connection)
        {
            mSession.login(username, password, url, connection); //Login



        }
        public void disconnect()
        {
            mSession.logout();
        }
        public void removeSession()
        {
            mSession.unsubscribeSessionStatus(this);
            mSession.unsubscribeResponse(this);
            mSession.Dispose();
        }
        #endregion

        #region Subscribe/Unsubscribe events
        public void subscibeForEvents()
        {
            // grab a table manager
            tableManager = mSession.getTableManager();
            O2GTableManagerStatus managerStatus = tableManager.getStatus();
            while (managerStatus == O2GTableManagerStatus.TablesLoading)
            {
                Thread.Sleep(50);
                managerStatus = tableManager.getStatus();
            }
            logger.Info("tableManagerStatus: " + tableManager.getStatus().ToString());
            if (managerStatus == O2GTableManagerStatus.TablesLoadFailed) //Have to handle this better at some point. 
                return;
            if (tableManager != null)
            {
                offersTbl = (O2GOffersTable)tableManager.getTable(O2GTableType.Offers);

                O2GSessionDescriptorCollection descs = mSession.getTradingSessionDescriptors();
                foreach (O2GSessionDescriptor desc in descs)
                {
                    Console.WriteLine("'{0}' '{1}' '{2}' {3}", desc.Id, desc.Name, desc.Description, desc.RequiresPin);
                    logger.Debug("{0}, {1}, {2}, {3}", desc.Id, desc.Name, desc.Description, desc.RequiresPin);
                    serverName = desc.Name;
                }
                O2GResponse resp = mSession.getLoginRules().getSystemPropertiesResponse();
            }
        }
        public void unsubcribeEvents()
        {
            if (tableManager != null)
            {
                offersTbl = (O2GOffersTable)tableManager.getTable(O2GTableType.Offers);
                tableManager = null;
            }
        }
        #endregion

        #region Session Status Listener methods
        /// Function for Status changes - Status: Connected - Get Offers, Accounts; Request Closed Trades, Trades, Orders, Message
        public void onSessionStatusChanged(O2GSessionStatusCode code)
        {
            Console.WriteLine("status- " + code.ToString());
            logger.Debug("Connection Status: {0}", code.ToString());
            //HddMainForm.getInstance().setStatusLabel(code.ToString());


            if (code == O2GSessionStatusCode.Connected)
            {
                subscibeForEvents();
                Console.WriteLine("finished loading events");
                connectionStatus = O2GSessionStatusCode.Connected;
            }
            else if (code == O2GSessionStatusCode.Disconnected)
            {
                unsubcribeEvents();
                disconnect();
            }
        }
        /// React with LoginFailed, Exit Application
        public void onLoginFailed(string error)
        {
            Console.WriteLine("Login error " + error);
            logger.Debug("Login Error: {0}", error);
            this.error = error;
            connectionStatus = O2GSessionStatusCode.Unknown;
        }
        #endregion

        #region Table Listener methods
        public void onAdded(string rowID, O2GRow data)
        {
            //Do nothing here
        }

        public void onChanged(string rowID, O2GRow data)
        {
            //Do nothing here
        }

        public void onDeleted(string rowID, O2GRow data)
        {
            //Do nothing here
        }

        public void onEachRow(string rowID, O2GRow data)
        {
            Console.WriteLine("Each event");
        }

        public void onStatusChanged(O2GTableStatus status)
        {
            Console.WriteLine("non-session status change event " + status.ToString());
        }

        #endregion

        #region Response Listner methods
        public void onRequestCompleted(string requestID, O2GResponse response)
        {
            Console.WriteLine("Request completed ID: " + requestID);
            switch (response.Type)
            {
                case O2GResponseType.GetOffers:
                {
                    Console.WriteLine("Received offers");
                    logger.Debug("Received Offers");
                    LoginForm.getInstance().fillRatesGrid();
                    break;
                }
                case O2GResponseType.MarketDataSnapshot:
                {
                    //All threads are saved to the thread dictionary
                    if (HddMainFormBasic.ThreadDictionary.ContainsKey(requestID))
                    {

                        DataSetup dataSetup = HddMainFormBasic.ThreadDictionary[requestID];
                        dataSetup.processData(response, HddMainFormBasic.getInstance(), dataSetup);
                        HddMainFormBasic.ThreadDictionary.Remove(requestID);
                    }

                    break;
                }
            }
        }

        public void onRequestFailed(string requestID, string error)
        {
            Console.WriteLine("requestID: " + requestID + ", Error: " + error);
            logger.Debug("requestID: " + requestID + ", Error: " + error);
            if(HddMainFormBasic.ThreadDictionary.ContainsKey(requestID))
            {
                DataSetup ds = HddMainFormBasic.ThreadDictionary[requestID];
                ds.IsAlive = false;
                HddMainFormBasic.ThreadDictionary[requestID] = ds;
            }
        }
        private void threadError(object parameters)
        {
            error = (string)((object[])parameters)[0];
            string header = (string)((object[])parameters)[1];
            MessageBox.Show(error, header, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void onTablesUpdates(O2GResponse respse)
        {
            //Do Nothing here
        }
        #endregion
    }
}
