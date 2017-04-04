using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using fxcore2;
using NLog;
using System.Collections.Concurrent;
using HDD.Properties;

namespace HDD
{
    /// <summary>
    /// The below code is the backend after user has successfully logged into the servers.
    /// </summary>
    public partial class HddMainFormBasic : Form
    {
        private static HddMainFormBasic INSTANCE = null;
        private static readonly object _MainInstanceLocker = new object();
        
        private Hashtable timeIntervalHT;
        private Settings set = Properties.Settings.Default;
        private static TradingSession tss;
        private static LoginForm loginForm = new LoginForm();

        public static double offset;
        private DateTime startDate;
        private DateTime adjustedStartDate;
        private DateTime endDate;
        public static string typeOfData;
        public static string timeInterval;
        public static string fileFormat;
        public static string outputDirectory;
        public static string delimiter;
        public static string dateFormat;
        public static string extension;
        public static string currency;
        private static string passwordFromLogin;
        private static string usernameFromLogin;
        private static string accountTypeFromLogin;
        private static string statusLabel;
        private static string versionLabel;

        private long currentThreadTicks = 0;
        private List<double> testList = new List<double>();

        private static ConcurrentDictionary<int, string> temp = new ConcurrentDictionary<int, string>();
        private static ConcurrentBag<int> templist = new ConcurrentBag<int>();


        public static Dictionary<string, DataSetup> threadDictionary = new Dictionary<string, DataSetup>();
        public static List<DataSetup> threadStatusList = new List<DataSetup>();

        private Logger logger = LogManager.GetCurrentClassLogger();


        #region Constructor
        public static HddMainFormBasic getInstance()
        {
            lock (_MainInstanceLocker)
            {
                if (INSTANCE == null)
                    INSTANCE = new HddMainFormBasic();
                return INSTANCE;
            }
        }

        public static void setPassword(string password)
        {
            passwordFromLogin = password;
        }

        public static void setUsername(string username)
        {
            usernameFromLogin = username;
        }

        public static void setAccountType(string accountType)
        {
            accountTypeFromLogin = accountType;
        }



        public static void setTradingSession(TradingSession tradingSession)
        {
            tss = tradingSession;
        }

        public static void setCurrencyList(ConcurrentDictionary<int, string> tmp, ConcurrentBag<int> tList)
        {
            temp = tmp;
            templist = tList;
        }


        public static void setLoginForm(LoginForm loginF)
        {
            loginForm = loginF;
        }

        public static void setStatusLabel(string statusL)
        {
            statusLabel = statusL;
        }

        public static void setVersionLabel(string versionL)
        {
            versionLabel = versionL;
        }


        /// <summary>
        /// Instantiation of HDD Basic
        /// </summary>
        public HddMainFormBasic()
        {
            InitializeComponent();
            #region LoggedInState
            loggedInState();
            #endregion

            #region Initialize local
            mVersionLabel.Text = versionLabel;
            timeIntervalHT = new Hashtable();
            timeIntervalHT.Add("1 Minute", "m1");
            timeIntervalHT.Add("5 Minute", "m5");
            timeIntervalHT.Add("15 Minute", "m15");
            timeIntervalHT.Add("30 Minute", "m30");
            timeIntervalHT.Add("1 Hour", "H1");
            timeIntervalHT.Add("4 Hour", "H4");
            timeIntervalHT.Add("1 Day", "D1");
            timeIntervalHT.Add("1 Week", "W1");
            timeIntervalHT.Add("1 Month", "M1");


            logger.Debug("There are a total of {0} instruments", templist.Count);
            for (int i = 0; i < templist.Count; i++)
            {
                myCurrency.Items.Add(temp[templist.ToArray()[i]]);
            }
            myCurrency.Text = set.currency;
            myUsername.Text = usernameFromLogin;
            myPassword.Text = passwordFromLogin;
            myConnection.Text = accountTypeFromLogin;

            myStatusLabel.Text = statusLabel;
            myStatusLabel.ForeColor = Color.Green;

            myTypeOfData.Text = set.typeofdata;
            myTimeInterval.Text = set.timeinterval;
            myOutputDirectory.Text = set.outputpath;

            if (set.startdate.Equals(""))
                myStartDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
            else
                try
                {
                    myStartDate.Text = set.startdate;
                }
                catch (Exception e) {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    throw e;
                }
            if (set.starttime.Equals(""))
                myStartTime.Text = "05:00:00 PM";
            else
                try
                {
                    myStartTime.Text = set.starttime;
                }
                catch (Exception e) {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    throw e;
                }
            if (set.enddate.Equals(""))
                myEndDate.Text = DateTime.Now.ToShortDateString();
            else
                try
                {
                    myEndDate.Text = set.enddate;
                }
                catch (Exception e) {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    throw e;
                }
            if (set.endtime.Equals(""))
                myEndTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            else
                try
                {
                    myEndTime.Text = set.endtime;
                }
                catch (Exception e) {
                    logger.Error(e.Message);
                    logger.Error(e.StackTrace);
                    throw e;
                }

            #endregion
        }
        #endregion

        #region delegates
        delegate void SetIntegerCallback(int i);
        delegate void SetNullCallback();
        delegate void SetBoolCallback(bool b);
        delegate void SetDoubleCallback(double d);
        delegate void SetTextCallback(string s);

        delegate void SetElementVisibilityCallback(bool b);
        
        #endregion

        #region Application Events
        private void myLogoutButton_Click(object sender, EventArgs e)
        {
            if (tss != null)
            {
                this.Hide();
                tss.disconnect();
                logger.Debug("Logged out");
                loginForm.Show();
            }
        }
        

        private void myBrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            string path = folderBrowserDialog1.SelectedPath;
            myOutputDirectory.Text = path;
        }

        //User clicking start download button
        private void myStartDownloadButton_Click(object sender, EventArgs e)
        {
            //Clear local cache everytime start button is pressed
            DataBaseHandler.getInstance().removeAllEntries();

            //This checks the text on the start download button and changes the state of it depending
            //on what the current state is
            if (myStartDownloadButton.Text.Equals("Abort"))
            {
                myProgressBar.Visible = false;
                logger.Debug("Abort Pressed");
                myStartDownloadButton.Text = "Start Download";
                

                if (backgroundWorker1.IsBusy)
                    backgroundWorker1.CancelAsync();

                loggedInState();
                DataSetup.keepThreadRunning = false;
                


            }
            else if (myStartDownloadButton.Text.Equals("Start Download"))
            {

                logger.Debug("Start Download was Pressed");
                DataSetup.keepThreadRunning = true;
                //Lot of error checking
                offset = 0;
                try
                {
                    offset = Double.Parse("0");
                }
                catch (Exception)
                {
                    MessageBox.Show("Offset field has to be numeric", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (DateTime.Parse(myStartDate.Text + " " + myStartTime.Text, Thread.CurrentThread.CurrentCulture) >= DateTime.Parse(myEndDate.Text + " " + myEndTime.Text, Thread.CurrentThread.CurrentCulture))
                {
                    MessageBox.Show("Start time must be before End time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (DateTime.Parse(myStartDate.Text + " " + myStartTime.Text, Thread.CurrentThread.CurrentCulture) >= DateTime.UtcNow.AddHours(offset))
                {
                    MessageBox.Show("Start time cannot be in the future", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (DateTime.Parse(myEndDate.Text + " " + myEndTime.Text) >= DateTime.UtcNow.AddHours(offset))
                {
                    MessageBox.Show("End time cannot be in the future", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (myCurrency.Text.Equals("") || myOutputDirectory.Text.Equals("") || myTimeInterval.Text.Equals("") || myTypeOfData.Text.Equals(""))
                {
                    MessageBox.Show("Please fill out all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                myProgressBar.Visible = Visible;
                myStartDownloadButton.Text = "Abort";
                //Set download state
                startedDownloadState();

                string delimit = "";
                timeInterval = myTimeInterval.Text; 
                timeInterval = (string)timeIntervalHT[timeInterval];

                delimit = ",";
                dateFormat = "MM/dd/yyyy,HH:mm:ss";
       
                extension = ".csv";
                   

                currency = myCurrency.Text;
                startDate = DateTime.Parse(myStartDate.Text + " " + myStartTime.Text).AddHours(offset * (-1));
                endDate = DateTime.Parse(myEndDate.Text + " " + myEndTime.Text).AddHours(offset * (-1));
                adjustedStartDate = startDate.AddMinutes(-10);
                if ((adjustedStartDate.DayOfWeek == DayOfWeek.Sunday && adjustedStartDate.Hour < 17) ||
                (adjustedStartDate.DayOfWeek == DayOfWeek.Saturday) ||
                (adjustedStartDate.DayOfWeek == DayOfWeek.Friday && adjustedStartDate.Hour > 15))
                {
                    adjustedStartDate = adjustedStartDate.AddDays(-2);
                }
                typeOfData = myTypeOfData.Text;

                delimiter = delimit;
                logger.Debug("Requesting " + myTimeInterval.Text + " data from: " + myStartDate.Text + " " + myStartTime.Text + " to " + myEndDate.Text + " " + myEndTime.Text);

                int maxInterations = 100;


                fileFormat = "CSV"; 
                outputDirectory = myOutputDirectory.Text;
                delimiter = delimit;

                //Quit if the user tries to do more than 5 years for 1 minute data
                TimeSpan ts = endDate - startDate;

                if (ts.TotalDays > 1800 && timeInterval.Equals("m1"))
                {
                    MessageBox.Show("Date range too big, please use smaller range!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pressAbort();
                    return;
                }

                DataSetup dataSetup = new DataSetup(startDate, 
                    endDate, 
                    tss, 
                    true, 
                    maxInterations, 
                    endDate.Ticks - startDate.Ticks,
                    typeOfData,
                    timeInterval,
                    fileFormat,
                    outputDirectory,
                    delimiter,
                    dateFormat,
                    extension,
                    currency);
                threadStatusList.Add(dataSetup);
                dataSetup.prepareDownload();
                Thread thread = new Thread(new ThreadStart(() => sendRequest(startDate, endDate, dataSetup)));
                thread.Name = "HDD_BASIC_THREAD";
                thread.Start();
                thread.Join();



                progressBarSetValue(1);
                backgroundWorker1.RunWorkerAsync();//this invokes the DoWork event




            }
        }

        /// <summary>
        /// Send request to API for data
        /// </summary>
        /// <param name="adjustedStartDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dataSetup"></param>
        public void sendRequest(DateTime adjustedStartDate, DateTime endDate, DataSetup dataSetup)
        {
            try
            {
                O2GRequestFactory factory = tss.Session.getRequestFactory();
                O2GTimeframeCollection timeframes = factory.Timeframes;
                O2GTimeframe tfo = timeframes[timeInterval];
                O2GRequest request = factory.createMarketDataSnapshotRequestInstrument(currency, tfo, 300);
                factory.fillMarketDataSnapshotRequestTime(request, adjustedStartDate, endDate, true);
                Console.WriteLine("Requesting from " + adjustedStartDate.ToString("MM/dd/yyyy HH:mm:ss") + " to " + endDate.ToString("MM/dd/yyyy HH:mm:ss"));
                logger.Debug("Requesting from " + adjustedStartDate.ToString("MM/dd/yyyy HH:mm:ss") + " to " + endDate.ToString("MM/dd/yyyy HH:mm:ss"));

                tss.Session.sendRequest(request);
                threadDictionary.Add(request.RequestID, dataSetup);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        /// <summary>
        /// If user clicks close, disconnect and set values to cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (tss != null)
            {
                tss.disconnect();
                tss.removeSession();
                tss = null;
                logger.Info("Logging Out");
                Thread.Sleep(1500);
            }
            set.username = myUsername.Text;
            set.password = myPassword.Text;
            set.connection = myConnection.Text;
            set.typeofdata = myTypeOfData.Text;
            set.timeinterval = myTimeInterval.Text;
            set.outputpath = myOutputDirectory.Text;
            set.startdate = myStartDate.Text;
            set.starttime = myStartTime.Text;
            set.enddate = myEndDate.Text;
            set.endtime = myEndTime.Text;
            set.currency = myCurrency.Text;
            set.Save();
            logger.Debug("Closing Application");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Application.Exit();



        }
        #endregion

        #region State
        private void startedDownloadState()
        {
            myLogoutButton.Enabled = false;
            myUsername.Enabled = false;
            myPassword.Enabled = false;
            myConnection.Enabled = false;
            myStartDate.Enabled = false;
            myStartTime.Enabled = false;
            myEndDate.Enabled = false;
            myEndTime.Enabled = false;
            myCurrency.Enabled = false;
            myTypeOfData.Enabled = false;
            myTimeInterval.Enabled = false;
            myOutputDirectory.Enabled = false;
            myBrowseButton.Enabled = false;
        }

        private void loggedInState()
        {
            myLogoutButton.Enabled = true;
            myUsername.Enabled = false;
            myPassword.Enabled = false;
            myConnection.Enabled = false;
            myStartDate.Enabled = true;
            myStartTime.Enabled = true;
            myEndDate.Enabled = true;
            myEndTime.Enabled = true;
            myCurrency.Enabled = true;
            myTypeOfData.Enabled = true;
            myTimeInterval.Enabled = true;
            myOutputDirectory.Enabled = true;
            myBrowseButton.Enabled = true;
            myStartDownloadButton.Enabled = true;
            myProgressBar.Value = 1;
            myPercentComplete.Enabled = false;
        }
        private void loggedOutState()
        {
            myLogoutButton.Enabled = false;
            myUsername.Enabled = true;
            myPassword.Enabled = true;
            myConnection.Enabled = true;
            myStartDate.Enabled = false;
            myStartTime.Enabled = false;
            myEndDate.Enabled = false;
            myEndTime.Enabled = false;
            myCurrency.Enabled = false;
            myTypeOfData.Enabled = false;
            myTimeInterval.Enabled = false;
            myOutputDirectory.Enabled = false;
            myBrowseButton.Enabled = false;
            myStartDownloadButton.Enabled = false;
        }
        #endregion

        #region Thread Safety for winforms
        public void pressAbort()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetNullCallback d = new SetNullCallback(pressAbort);
                    this.BeginInvoke(d);
                    return;
                }
                else
                {
                    logger.Debug("Download Complete");
                    if (myStartDownloadButton.Text.Equals("Abort"))
                        myStartDownloadButton_Click(new object(), new EventArgs());
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }
        }


        #region ProgressBar methods
        public void progressBarSetMaxStep(int i)
        {

            try
            {
                if (this.InvokeRequired)
                {
                    SetIntegerCallback d = new SetIntegerCallback(progressBarSetMaxStep);
                    this.BeginInvoke(d, i);
                    return;
                }
                else
                {
                    myProgressBar.Maximum = i;
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }

        }

        public void progressBarSetValue(int i)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetIntegerCallback d = new SetIntegerCallback(progressBarSetValue);
                    this.BeginInvoke(d, i);
                    return;
                }
                else
                {
                    if (i == 0)
                        i = 1;
                    myProgressBar.Visible = Visible;
                    myProgressBar.Value = i;
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }

        }



        public void progressBarVisibility(bool Value)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetElementVisibilityCallback d = new SetElementVisibilityCallback(progressBarVisibility);
                    this.BeginInvoke(d, Value);
                    return;
                }
                else
                {
                    myProgressBar.Visible = Value;
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }

        }

        public void setPercentComplete(string d)
        {


            try
            {
                if (this.InvokeRequired)
                {
                    SetTextCallback r = new SetTextCallback(setPercentComplete);
                    this.BeginInvoke(r, d);
                    return;
                }
                else
                {
                    myPercentComplete.Text = d;

                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }

        }



        public void setStatusType(string d)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetTextCallback r = new SetTextCallback(setStatusType);
                    this.BeginInvoke(r, d);
                    return;
                }
                else
                {
                    myStatusType.Text = d;
                }
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }
        }

        #endregion

        #endregion


        public static Dictionary<string, DataSetup> ThreadDictionary
        {
            get
            {
                return threadDictionary;
            }

            set
            {
                threadDictionary = value;
            }
        }

        /// <summary>
        /// Background worker to check if the threads are alive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime startTime = DateTime.Now;
            Stopwatch stopWatch = new Stopwatch();

            e.Result = "";
            int i = 0;


            foreach (DataSetup ds in threadStatusList)
            {
                while (ds.IsAlive)
                {


                    if (ds.CurrentStep != 0)
                    {
                        System.Threading.Thread.Sleep(1000);
                        backgroundWorker1.ReportProgress(i, ds);

                    }

                    if (backgroundWorker1.CancellationPending)
                    {
                        threadStatusList.Clear();
                        e.Cancel = true;
                        return;
                    }

                    ++i;
                };
            }
            MessageBox.Show("Download Complete", "Historical Data Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);

            progressBarSetValue(1);
            //Writing to file
            setPercentComplete("");
            setStatusType("Writing Data");

            List<Data> tempprices = null;
            tempprices = DataBaseHandler.getInstance().getEntries(typeOfData);

            setPercentComplete("100%");
            progressBarVisibility(Visible);



            DataSetup.writeToFile(tempprices, HddMainFormBasic.typeOfData, HddMainFormBasic.fileFormat, DataSetup.fileName, HddMainFormBasic.timeInterval, HddMainFormBasic.offset, HddMainFormBasic.delimiter, HddMainFormBasic.dateFormat);
            tempprices.Clear();
            DataBaseHandler.getInstance().removeAllEntries();
            threadStatusList.Clear();
            pressAbort();
        }

       /// <summary>
       /// Convert Microseconds to time
       /// </summary>
       /// <param name="microSec"></param>
       /// <returns></returns>
        private static TimeSpan FromMS(double microSec)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(microSec);

            return time;
        }

        /// <summary>
        /// This runs when background worker is complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            setPercentComplete("");
            progressBarVisibility(false);
            setStatusType("");
        }

        /// <summary>
        /// This runs when progress is being updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataSetup ds = new DataSetup();
            ds = (DataSetup)e.UserState;

            double currentStep = ds.CurrentStep;


            if (ds.VariableChanged)
            {
                currentThreadTicks = ds.Time.Ticks;
                ds.VariableChanged = false;
            }


            //Calculate the estimated time remainining
            //Formula: A = Total ticks for days / number of ticks per request
            //         Estimated Time = time it took per request * A;
            if (ds.Tick != 0)
            {
                try {
                    TimeSpan span = endDate.Subtract(startDate);
                    TimeSpan estimatedTime = TimeSpan.FromTicks(span.Ticks / ds.Tick);

                    TimeSpan countDownTime = TimeSpan.FromTicks(estimatedTime.Ticks * Convert.ToInt32(Math.Floor(currentStep)) / ds.MaxIterations);
                    setPercentComplete(countDownTime.ToString(@"hh\:mm\:ss"));
                } catch (InvalidOperationException ex)
                {
                    throw ex;
                }
            }

            if (ds.MaxIterations - Convert.ToInt32(Math.Floor(currentStep)) >= 0)
                progressBarSetValue(ds.MaxIterations - Convert.ToInt32(Math.Floor(currentStep)));
            else
                progressBarSetValue(1);

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void HddMainFormBasic_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }
    }
}
