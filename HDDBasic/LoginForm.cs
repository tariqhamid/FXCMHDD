using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;
using fxcore2;
using NLog;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace HDD
{
    public partial class LoginForm : Form
    {
        private Form currentForm;
        private HddMainFormBasic hddBasic;
        private List<LoginInfo> loginInfoList = new List<LoginInfo>();
        private SQLiteConnection m_dbConnection;
        private TradingSession tss;
        private static TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public string getPasswordTextbox() { return passwordTextbox.Text; }
        public void setPasswordTextbox(string password) { passwordTextbox.Text = password; }
        private static readonly object _MainInstanceLocker = new object();
        private static LoginForm INSTANCE = null;
        private ConcurrentDictionary<int, string> temp = new ConcurrentDictionary<int, string>();
        private ConcurrentBag<int> templist = new ConcurrentBag<int>();
        private Logger logger = LogManager.GetCurrentClassLogger();
        private Properties.Settings set = Properties.Settings.Default;
        private static readonly object databaseLock = new object();

        delegate void SetTextCallback(string s);
        delegate void SetNullCallback();






        public static LoginForm getInstance()
        {
            lock (_MainInstanceLocker)
            {
                if (INSTANCE == null)
                    INSTANCE = new LoginForm();
                return INSTANCE;
            }
        }

        public LoginForm()
        {
            logger.Debug("Starting HDD Application");
            try {
                InitializeComponent();
            } catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }
        }

   

        private void loginLogoutButton_Click(object sender, EventArgs e)
        {



            //Validate username and password
            if (usernameDropdownList.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please enter a valid username!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (passwordTextbox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please enter a valid password!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }




            //Instantiate trading session and try to login
            tss = new TradingSession();
            tss.login(usernameDropdownList.Text, passwordTextbox.Text, "http://www.fxcorporate.com/Hosts.jsp", accountTypeDropdownList.Text);
            //Log.logMessageToFile("Login","Login method called");
            logger.Debug("Login method was called");


            //Wait for 5000 milliseconds to make sure the application logged in
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (tss.ConnectionStatus != O2GSessionStatusCode.Connected)
            {
                Thread.Sleep(1000);
                if(tss.ConnectionStatus == O2GSessionStatusCode.Unknown)
                {
                    logger.Error("Connection Status is unknown");
                    MessageBox.Show("Please check user credentials", "Error logging in");
                    return;
                }

                if (timer.Elapsed.TotalSeconds > 90 && tss.ConnectionStatus != O2GSessionStatusCode.Connected)
                {
                    logger.Error("Login is taking longer than 20 seconds!");
                    MessageBox.Show("Please check user credentials", "Error logging in");
                    return;
                }
            }

            timer.Stop();


            //Check if any login errors occurred, if exist return and throw error.
            if (tss.Error.Length != 0)
            {
                MessageBox.Show("Please check user credentials", "Error logging in");
                return;
            }


            //this.Invoke((MethodInvoker)delegate () {

            //});

            this.Hide();
            fillRatesGrid();

            logger.Debug("Starting HDD Basic");
            HddMainFormBasic.setPassword(passwordTextbox.Text);
            HddMainFormBasic.setUsername(usernameDropdownList.Text);
            HddMainFormBasic.setAccountType(accountTypeDropdownList.Text);
            HddMainFormBasic.setTradingSession(tss);
            HddMainFormBasic.setStatusLabel("Connected");
            HddMainFormBasic.setCurrencyList(temp, templist);
            HddMainFormBasic.setVersionLabel(mVersionLabel.Text);
            hddBasic = new HddMainFormBasic();
            hddBasic.Show();


            if (saveLoginButton.Checked)
            {
                set.username = usernameDropdownList.Text;
                set.connection = accountTypeDropdownList.Text;
                set.saveMe = saveLoginButton.Checked;
                set.Save();
            }


            //Save the user information into SQL lite
            fillTable(usernameDropdownList.Text.Trim(), accountTypeDropdownList.SelectedItem.ToString());
        }


        //This function will fill the rates table and put it in a list to be accessed later
        internal void fillRatesGrid()
        {
            //This is to make sure the current thread is calling this function, if not call it agian
            if (INSTANCE.InvokeRequired)
            {
                try
                {
                    SetNullCallback callback = new SetNullCallback(fillRatesGrid);
                    Invoke(callback, new object[] { });
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    throw ex;
                }
            }
            else
            {
                //Get list of symbols from the offers table
                O2GTableIterator iterator = new O2GTableIterator();
                O2GOfferTableRow offerRow = null;
                if (tss != null)
                {
                    Thread.Sleep(3000);
                    O2GOffersTable offersTbl = tss.OffersTable;
                    if (offersTbl == null)
                    {
                        //Log.logMessageToFile("Login", "OfferTable null");
                        logger.Debug("OfferTable is empty");
                        return;
                    }

                    temp = new ConcurrentDictionary<int, string>();
                    templist = new ConcurrentBag<int>();
                    while (offersTbl.getNextRow(iterator, out offerRow))
                    {
                        if (offerRow.SubscriptionStatus.Equals("T")) {
                            templist.Add(Convert.ToInt32(offerRow.OfferID));
                            logger.Debug("Offer {0}; Symbol {1}", offerRow.OfferID, offerRow.Instrument);
                            temp.TryAdd(Convert.ToInt32(offerRow.OfferID), offerRow.Instrument);
                        }
                    }
                }
            }
        }

        //basic message logging function to log file
        private static readonly object _logFileLocker = new object();

        public ConcurrentDictionary<int, string> Temp
        {
            get
            {
                return temp;
            }

            set
            {
                temp = value;
            }
        }

        public ConcurrentBag<int> Templist
        {
            get
            {
                return templist;
            }

            set
            {
                templist = value;
            }
        }

        //If user has focus on the username box, it will blank out placeholder text
        private void username_Enter(object sender, EventArgs e)
        {
            if (usernameDropdownList.Text == "Username")
            {
                usernameDropdownList.Text = "";
            }
        }

        //If user leaves focus on the username box, it will put placeholder text back assuming the field was ""
        private void username_Leave(object sender, EventArgs e)
        {
            if (usernameDropdownList.Text == "")
            {
                usernameDropdownList.Text = "Username";
            }
        }
        //if the user double clicks the notify icon button, the form will become active
        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }

        }

        //Close the program is user clicks close on the notify icon
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// This will load all default functionalities for Login Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            logger.Info("Starting LoginForm_Load");

            try {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Text = "FXCM Historical Data Downloader Basic";


                accountTypeDropdownList.Items.Add("Real");
                accountTypeDropdownList.Items.Add("Demo");
                accountTypeDropdownList.DropDownStyle = ComboBoxStyle.DropDownList;
                accountTypeDropdownList.Text = "Real";

                //This creates/gets data from the cache(SQL Lite)
                currentForm = this;
                try
                {
                    connectToDatabase();
                    createTable();
                    getData();
                    fillDropDown();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                    throw ex;
                }

                mVersionLabel.Text = "Version: " + ProductVersion;

                if (set.username.Trim().Length != 0)
                {
                    usernameDropdownList.Text = set.username;
                    accountTypeDropdownList.SelectedItem = set.connection;
                    saveLoginButton.Checked = set.saveMe;
                }

                logger.Info("Loading Settings, Username: {0}, Connection Type: {1}, Save Login {2}",
                    usernameDropdownList.Text, accountTypeDropdownList.SelectedItem, saveLoginButton.Checked);
            } catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }
        }


        //Fill the username dropdown list from the cache
        private void fillDropDown()
        {
            foreach (LoginInfo list in loginInfoList)
            {
                usernameDropdownList.Items.Add(list.Username);
            }
            //Make drop down list first element
            if (loginInfoList.Count > 0)
            {
                usernameDropdownList.Text = loginInfoList[0].Username;
                accountTypeDropdownList.Text = loginInfoList[0].AccountType;
            }



        }


        // Creates a connection with our database file.
        void connectToDatabase()
        {
            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FXCM\\HDD\\LOGIN_INFO.sqlite;Version=3;");
                lock (databaseLock)
                {
                    m_dbConnection.Open();
                }
            } catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }
        }

        // Creates a named LoginInfo that stores user information from previous sessions
        void createTable()
        {
            try
            {
                using (SQLiteCommand mCmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS [LOGININFO] (id INTEGER PRIMARY KEY AUTOINCREMENT, 'Username' TEXT, 'AccountType' TEXT, 'RegistrationKey' TEXT);", m_dbConnection))
                {
                    mCmd.ExecuteNonQuery();
                }
            } catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }
        }


        //Stores user information into the LOGININFO cache but first we have to check if it already exist
        void fillTable(string username, string accountType)
        {
            logger.Debug("Inserting into local db");
            //Check if the username already exist, if it does, return and do not add
            string sql = "SELECT * FROM LOGININFO WHERE USERNAME = '" + username + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            //Insert if new, else update
            if (!reader.HasRows)
            {
                if (saveLoginButton.Checked)
                {
                    logger.Debug("Saving Username: {0}, AccountType: {1}", username, accountType);
                    sql = "INSERT INTO LOGININFO (Username, AccountType) values ('" + username + "', '" + accountType + "')";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                if (saveLoginButton.Checked)
                {
                    logger.Debug("Updating Username: {0}, AccountType: {1}", username, accountType);
                    sql = "UPDATE LOGININFO SET AccountType = '" + accountType + "' WHERE Username = '" + username + "'";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
            }



        }

        //Get data from the cache
        void getData()
        {
            string sql = "SELECT * FROM LOGININFO ORDER BY ID DESC";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                LoginInfo logInfo = new LoginInfo();
                Console.WriteLine("Fetching Username: {0}, AccountType: {1}, Registration Key: {2} from local DB", reader["Username"], reader["AccountType"], reader["RegistrationKey"]);
                logger.Debug("Fetching Username: {0}, AccountType: {1}, Registration Key: {2} from local DB", reader["Username"], reader["AccountType"], reader["RegistrationKey"]);
                logInfo.Username = reader["Username"].ToString();
                logInfo.AccountType = reader["AccountType"].ToString();
                loginInfoList.Add(logInfo);

            }

            Console.ReadLine();
        }

        //Show a popup if the user wants to exit the application
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();

        }

        //Close form if the user clicks the notify icon 'Close'
        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        //Link label for 'Register'
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.register.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.fxcm.com/global-demo-account-portal/");
        }

        //Set form state to normal if user clicks the notify icon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;

        }

        //If user Minimizes the application, the application will Minimize to notify icon (Lower right hand corner icon)
        private void LoginForm_Resize(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void closeToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void passwordTextbox_Enter(object sender, EventArgs e)
        {
            if (passwordTextbox.Text.Trim() == "Password")
            {
                passwordTextbox.Text = "";
                passwordTextbox.PasswordChar = '*';
            }

        }

        private void passwordTextbox_Leave(object sender, EventArgs e)
        {
            if (passwordTextbox.Text.Trim() == "")
            {
                passwordTextbox.Text = "Password";

                string a = passwordTextbox.Text;
                passwordTextbox.PasswordChar = '\0';
            }
        }

        private void usernameDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usernameDropdownList.SelectedItem != null)
            {
                string currentItem = usernameDropdownList.SelectedItem.ToString();
                foreach (LoginInfo list in loginInfoList)
                {
                    if (list.Username.Equals(currentItem))
                    {
                        accountTypeDropdownList.Text = list.AccountType;
                    }
                }
            }
        }
    }

    //Login Info class
    public class LoginInfo
    {
        private string username;
        private string accountType;

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string AccountType
        {
            get
            {
                return accountType;
            }

            set
            {
                accountType = value;
            }
        }

    }



}
