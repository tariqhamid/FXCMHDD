using fxcore2;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HDD
{
    public class DataSetup
    {
        private DateTime startDate;
        private DateTime endDate;
        public static string fileName;

        private TradingSession tss;
        private bool isAlive;
        private int maxIterations = 100;
        private long initialTotalTicks;
        private double currentStep;
        private List<Data> prices = new List<Data>();
        private long tick;
        private TimeSpan time;
        public static Stopwatch sw;

        private bool variableChanged = true;
        private long currentThreadTicks = 0;

        private List<double> currentStepList = new List<double>();
        private string typeOfData;
        private string timeInterval;
        private string fileFormat;
        private string outputDirectory;
        private string delimiter;
        private string dateFormat;
        private string extension;
        private string currency;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private bool callBack = false;

        public static bool keepThreadRunning = true;


        public DateTime StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }

            set
            {
                isAlive = value;
            }
        }

        public int MaxIterations
        {
            get
            {
                return maxIterations;
            }

            set
            {
                maxIterations = value;
            }
        }

        public long InitialTotalTicks
        {
            get
            {
                return initialTotalTicks;
            }

            set
            {
                initialTotalTicks = value;
            }
        }


        public double CurrentStep
        {
            get
            {
                return currentStep;
            }

            set
            {
                currentStep = value;
            }
        }

        public long Tick
        {
            get
            {
                return tick;
            }

            set
            {
                tick = value;
            }
        }

        public TimeSpan Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public bool VariableChanged
        {
            get
            {
                return variableChanged;
            }

            set
            {
                variableChanged = value;
            }
        }

        public bool KeepThreadRunning
        {
            get
            {
                return keepThreadRunning;
            }

            set
            {
                keepThreadRunning = value;
            }
        }

        public bool CallBack
        {
            get
            {
                return callBack;
            }

            set
            {
                callBack = value;
            }
        }

        public DataSetup() { }

        public DataSetup(DateTime sDate, DateTime eDate, TradingSession tradingSession)
        {
            startDate = sDate;
            endDate = eDate;
            tss = tradingSession;
            variableChanged = true;
        }

        public DataSetup(DateTime sDate, 
            DateTime eDate, 
            TradingSession tradingSession,
            bool isAl,
            int maxIter,
            long initialTicks,
            string typeOfData,
            string timeInterval,
            string fileFormat,
            string outputDirectory,
            string delimiter,
            string dateFormat,
            string extension, 
            string currency)
        {
            startDate = sDate;
            endDate = eDate;
            tss = tradingSession;
            isAlive = isAl;
            maxIterations = maxIter;
            initialTotalTicks = initialTicks;
            sw = Stopwatch.StartNew();
            variableChanged = true;
            this.typeOfData = typeOfData;
            this.timeInterval = timeInterval;
            this.fileFormat = fileFormat;
            this.outputDirectory = outputDirectory;
            this.delimiter = delimiter;
            this.dateFormat = dateFormat;
            this.extension = extension;
            this.currency = currency;


        }

    

        #region prepare file to be created.
        public void prepareDownload()
        {

            DataBaseHandler.getInstance().removeAllEntries();
            if (Directory.Exists(outputDirectory) == false)
            {
                try
                {
                    Directory.CreateDirectory(outputDirectory);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (fileFormat.Equals("Ninja") == false)
                fileName = outputDirectory + "\\" + currency.Replace("/", "") + "_" + timeInterval + "_" + typeOfData + extension;
            else
                fileName = outputDirectory + "\\$" + currency.Replace("/", "") + "." + typeOfData + "_" + extension;
            //Log.logMessageToFile("Thread", "startDate: " + startDate + " endDate: " + endDate + " typeOfData: " + typeOfData + " timeInterval:" + timeInterval + " fileFormat: " + fileFormat + " timeZone: " + " outputDirectory: " + outputDirectory);
            logger.Debug("startDate: " + startDate + " endDate: " + endDate + " typeOfData: " + typeOfData + " timeInterval:" + timeInterval + " fileFormat: " + fileFormat + " timeZone: " + " outputDirectory: " + outputDirectory);
            try
            {
                File.Delete(fileName);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                logger.Error(e.StackTrace);
                throw e;
            }
            using (StreamWriter file = new StreamWriter(@fileName))
            {
                if (fileFormat.Equals("CSV") || fileFormat.Equals("ST") || fileFormat.Equals("Custom"))
                {
                    if (timeInterval.Equals("t1") == false)
                    {
                        if (typeOfData.Equals("Bid"))
                            file.WriteLine("\"Date\"" + delimiter + "\"Time\"" + delimiter + "\"Open\"" + delimiter + "\"High\"" + delimiter + "\"Low\"" + delimiter + "\"Close\"" + delimiter + "\"Total Ticks\"");
                        else if (typeOfData.Equals("Ask"))
                            file.WriteLine("\"Date\"" + delimiter + "\"Time\"" + delimiter + "\"Open\"" + delimiter + "\"High\"" + delimiter + "\"Low\"" + delimiter + "\"Close\"" + delimiter + "\"Total Ticks\"");
                        else
                            file.WriteLine("\"Date\"" + delimiter + "\"Time\"" + delimiter + "\"OpenBid\"" + delimiter + "\"HighBid\"" + delimiter + "\"LowBid\"" + delimiter + "\"CloseBid\"" + delimiter + "\"OpenAsk\"" + delimiter + "\"HighAsk\"" + delimiter + "\"LowAsk\"" + delimiter + "\"CloseAsk\"" + delimiter + "\"Total Ticks\"");
                    }
                    else
                    {
                        if (typeOfData.Equals("Bid"))
                            file.WriteLine("\"Date\"" + delimiter + "\"Time\"" + delimiter + "\"Bid\"");
                        else if (typeOfData.Equals("Ask"))
                            file.WriteLine("\"Date\"" + delimiter + "\"Time\"" + delimiter + "\"Ask\"");
                        else
                            file.WriteLine("\"Date\"" + delimiter + "\"Time\"" + delimiter + "\"Bid\"" + delimiter + "\"Ask\"");
                    }
                }
                else if (fileFormat.Equals("MT4"))
                {

                }
            }

            maxIterations = 100;
            initialTotalTicks = endDate.Ticks - startDate.Ticks;
            currentStep = maxIterations;
        }
        #endregion


        #region process data for HddMainFormBasic
        public void processData(O2GResponse response, HddMainFormBasic hdd, DataSetup dataSetup)
        {
            bool outOfRangeDateFound = false;
            DateTime tempEndDate = new DateTime();

            try
            {
                //Exit thread if the value is false
                if (!keepThreadRunning)
                {
                    return;
                }

                var query1 = HddMainFormBasic.threadDictionary.Keys.FirstOrDefault(t => t == response.RequestID);
                if (query1 != null)
                {
                    HddMainFormBasic.threadDictionary[query1].CallBack = true;
                }


                O2GResponseReaderFactory readerfactory = tss.Session.getResponseReaderFactory();

                O2GMarketDataSnapshotResponseReader reader = readerfactory.createMarketDataSnapshotReader(response);

                DateTime from = new DateTime();
                DateTime to = new DateTime();
                if (reader.Count > 0)
                {
                    from = reader.getDate(0);
                    to = reader.getDate(reader.Count - 1);
                }
                Console.WriteLine("Market data received from " + from.ToString("MM/dd/yyyy HH:mm:ss") + " to " + to.ToString("MM/dd/yyyy HH:mm:ss"));

                //insert data into price list
                for (int j = reader.Count - 1; j > -1; j--)
                {
                    prices.Add(new Data(reader.getAskOpen(j), reader.getAskHigh(j), reader.getAskLow(j), reader.getAskClose(j), reader.getBidOpen(j), reader.getBidHigh(j), reader.getBidLow(j), reader.getBidClose(j), reader.getDate(j), reader.getVolume(j)));
                }
                //if less rates are obtained then it means we reached the end.
                //Nothing came back for the timeframe specified means, nothing exist in the remaining timeframe.
                if (reader.Count < 300 || reader.Count == 0)
                {
                    outOfRangeDateFound = true;
                    tempEndDate = endDate;
                }

                if (!outOfRangeDateFound)
                {
                    removeLastDate(prices[prices.Count - 1].StartTime, prices);

                    tempEndDate = prices[prices.Count - 1].StartTime;
                }
                if (prices.Count > 3000)
                {
                    DataBaseHandler.getInstance().addEntries(prices);
                    prices.Clear();
                    if (timeInterval.Equals("t1"))
                        Thread.Sleep(2000);
                }

                //Get total ticks between end date and start date
                long totalTicks = tempEndDate.Ticks - dataSetup.StartDate.Ticks;

                currentStep = 0;
                currentStep = ((double)(totalTicks * maxIterations)) / initialTotalTicks;
                dataSetup.currentStep = currentStep;
                tick = reader.Count;
                Time = sw.Elapsed;

                #region repeat process OR quit
                if (outOfRangeDateFound == false)
                {
                    try
                    {
                        //Call HDD function to calculate more data
                        DataSetup ds = new DataSetup();
                        ds = dataSetup;

                        ds.EndDate = tempEndDate;
                        hdd.sendRequest(startDate, tempEndDate, ds);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e.Message);
                    }

                }
                else
                {
                    Console.WriteLine("Done");

                    #region Final flushing to database
                    try
                    {
                        if (prices.Count > 0)
                        {
                            //Finally write to database
                            DataBaseHandler.getInstance().addEntries(prices);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error in writing data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        logger.Debug(e.Message);
                        logger.Debug(e.StackTrace);
                    }
                    finally
                    {
                        prices.Clear();
                    }
                    #endregion

                    #region Writing to a File
                    long rows = 0;
                    rows = DataBaseHandler.getInstance().getNumberOfRows();

                    //Calculate the levels first before requesting data.
                    long templevel = 0;
                    List<long> levels = new List<long>();
                    levels.Add(0);
                    while (true)
                    {
                        if (templevel + 5000 < rows)
                        {
                            templevel += 5000;
                            levels.Add(templevel);
                        }
                        else
                        {
                            levels.Add(rows);
                            break;
                        }
                    }
                    logger.Debug("Levels of data to be requested from database: " + levels.ToString());
                    dataSetup.isAlive = false;
                    #endregion
                }
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.Source, "Error");
            }
        }
        #endregion

        //This will remove the last date
        public static void removeLastDate(DateTime date, List<Data> prices)
        {
            for (int i = 0; i < prices.Count; i++)
            {
                if (compareDates(date, prices[i].StartTime) == 0)
                {
                    prices.RemoveAt(i);
                    //Log.logMessageToFile("Thread", "Removing price at: " + date);
                    logger.Debug("Removing price at: " + date);
                }
            }
        }

        //Compares two dates
        // if A == B return 0
        // if A < B  return -1
        // If A > B  return 1
        public static int compareDates(DateTime a, DateTime b)
        {
            DateTime d1 = new DateTime(a.Year, a.Month, a.Day, a.Hour, a.Minute, a.Second, a.Millisecond);
            DateTime d2 = new DateTime(b.Year, b.Month, b.Day, b.Hour, b.Minute, b.Second, b.Millisecond);
            if (d1 == d2)
                return 0;
            if (d1 < d2)
                return -1;
            else
                return 1;
        }

        //Writes to file function 
        public static void writeToFile(List<Data> prices, string typeOfData, string fileFormat, string fileName, string timeInterval, double offset, string delimiter, string dateformat)
        {
            using (StreamWriter file = new StreamWriter(@fileName, true))
            {
                if (fileFormat.Equals("ST") || fileFormat.Equals("CSV") || fileFormat.Equals("Custom"))
                {
                    if (timeInterval.Equals("t1"))
                    {
                        for (int i = 0; i < prices.Count; i++)
                        {
                            if (typeOfData.Equals("Bid"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter + prices[i].BidClose);
                            else if (typeOfData.Equals("Ask"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter + prices[i].AskClose);
                            else
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter + prices[i].BidClose + delimiter + prices[i].AskClose);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < prices.Count; i++)
                        {
                            if (typeOfData.Equals("Bid"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                    prices[i].BidOpen + delimiter + prices[i].BidHigh + delimiter + prices[i].BidLow + delimiter + prices[i].BidClose + delimiter + prices[i].Volume);
                            else if (typeOfData.Equals("Ask"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                    prices[i].AskOpen + delimiter + prices[i].AskHigh + delimiter + prices[i].AskLow + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                            else
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                    prices[i].BidOpen + delimiter + prices[i].BidHigh + delimiter + prices[i].BidLow + delimiter + prices[i].BidClose + delimiter + prices[i].AskOpen + delimiter + prices[i].AskHigh + delimiter + prices[i].AskLow + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                        }
                    }
                }
                else if (fileFormat.Equals("MT4"))
                {
                    for (int i = 0; i < prices.Count; i++)
                    {
                        if (typeOfData.Equals("Bid"))
                            file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                prices[i].BidOpen + delimiter + prices[i].BidHigh + delimiter + prices[i].BidLow + delimiter + prices[i].BidClose + delimiter + prices[i].Volume);
                        else if (typeOfData.Equals("Ask"))
                            file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                prices[i].AskOpen + delimiter + prices[i].AskHigh + delimiter + prices[i].AskLow + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                        else
                            file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                prices[i].BidOpen + delimiter + prices[i].BidHigh + delimiter + prices[i].BidLow + delimiter + prices[i].BidClose + delimiter + prices[i].AskOpen + delimiter + prices[i].AskHigh + delimiter + prices[i].AskLow + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                    }
                }
                else if (fileFormat.Equals("Ninja"))
                {
                    if (timeInterval.Equals("t1"))
                    {
                        for (int i = 0; i < prices.Count; i++)
                        {
                            if (typeOfData.Equals("Bid"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter + prices[i].BidClose + delimiter + prices[i].Volume);
                            else if (typeOfData.Equals("Ask"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                            else
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter + prices[i].BidClose + delimiter + prices[i].Volume);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < prices.Count; i++)
                        {
                            if (typeOfData.Equals("Bid"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                    prices[i].BidOpen + delimiter + prices[i].BidHigh + delimiter + prices[i].BidLow + delimiter + prices[i].BidClose + delimiter + prices[i].Volume);
                            else if (typeOfData.Equals("Ask"))
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                    prices[i].AskOpen + delimiter + prices[i].AskHigh + delimiter + prices[i].AskLow + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                            else
                                file.WriteLine(prices[i].StartTime.AddHours(offset).ToString(dateformat) + delimiter +
                                    prices[i].BidOpen + delimiter + prices[i].BidHigh + delimiter + prices[i].BidLow + delimiter + prices[i].BidClose + delimiter + prices[i].AskOpen + delimiter + prices[i].AskHigh + delimiter + prices[i].AskLow + delimiter + prices[i].AskClose + delimiter + prices[i].Volume);
                        }
                    }
                }
            }
        }



    }
}
