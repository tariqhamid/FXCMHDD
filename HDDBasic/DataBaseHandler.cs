using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using NLog;

namespace HDD
{
    class DataBaseHandler
    {
        private SQLiteConnection sqlConn;
        private SQLiteDataAdapter sqlAdapter;
        private SQLiteCommand sqlCommand;
        private static readonly object databaseLock = new object();
        private static DataBaseHandler INSTANCE;
        private DataSet dataSet;
        private DataTable dataTable;
        private static readonly object _lock = new object();
        private long index = 0;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static DataBaseHandler getInstance()
        {
            lock (_lock)
            {
                if (INSTANCE == null)
                    INSTANCE = new DataBaseHandler();
                return INSTANCE;
            }
        }
        public DataBaseHandler()
        {
            initatiateClassUponLogin();
        }
        ~DataBaseHandler()
        {
            //Log.getInstance().LogMessageToFile("DataBaseHandler Closed");
        }

        /// <summary>
        /// Create local databse file for use later
        /// </summary>
        public void initatiateClassUponLogin()
        {
            try
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FXCM\\HDD\\Data.sqlite");

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FXCM\\HDD\\Data.sqlite"))
                {
                    sqlConn = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FXCM\\HDD\\Data.sqlite;Version=3;;New=True;Compress=True");
                    sqlConn.Open();
                }
                else
                {
                    sqlConn = new SQLiteConnection("Data Source=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FXCM\\HDD\\Data.sqlite;Version=3;New=True;Compress=True;");
                    lock (databaseLock)
                    {
                        sqlConn.Open();
                        sqlCommand = sqlConn.CreateCommand();
                        sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS PriceTable(Inx integer primary key AUTOINCREMENT, Date TEXT NOT NULL, Dt TEXT, BidOpen TEXT NOT NULL, BidHigh TEXT NOT NULL, BidLow TEXT NOT NULL, BidClose TEXT NOT NULL, AskOpen TEXT NOT NULL, AskHigh TEXT NOT NULL, AskLow TEXT NOT NULL, AskClose TEXT NOT NULL, Volume TEXT NOT NULL);";
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                throw ex;
            }
        }


        /// <summary>
        /// Insert new row
        /// </summary>
        /// <param name="date"></param>
        /// <param name="bidopen"></param>
        /// <param name="bidhigh"></param>
        /// <param name="bidlow"></param>
        /// <param name="bidclose"></param>
        /// <param name="askopen"></param>
        /// <param name="askhigh"></param>
        /// <param name="asklow"></param>
        /// <param name="askclose"></param>
        /// <param name="volume"></param>
        public void addEntry(DateTime date, double bidopen, double bidhigh, double bidlow, double bidclose, double askopen, double askhigh, double asklow, double askclose, int volume)
        {
            lock (databaseLock)
            {
                //index++;
                sqlCommand = sqlConn.CreateCommand();
                sqlCommand.CommandText = "BEGIN; "+
                        "INSERT INTO PriceTable(Date, BidOpen, BidHigh, BidLow, BidClose, AskOpen, AskHigh, AskLow, AskClose, Volume)" +
                        " VALUES('" + date.Ticks + "','" + bidopen + "','" + bidhigh + "','" + bidlow + "','" + bidclose + "','" + askopen + "','" + askhigh + "','" + asklow + "','" + askclose + "','" + volume + "'); COMMIT;";
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Insert in bulk
        /// </summary>
        /// <param name="prices"></param>
        public void addEntries(List<Data> prices)
        {
            lock (databaseLock)
            {
                SQLiteBulkInsert sbi = new SQLiteBulkInsert(sqlConn, "PriceTable");
                sbi.AddParameter("Date", DbType.String);
                sbi.AddParameter("Dt", DbType.String);
                sbi.AddParameter("BidOpen", DbType.String);
                sbi.AddParameter("BidHigh", DbType.String);
                sbi.AddParameter("BidLow", DbType.String);
                sbi.AddParameter("BidClose", DbType.String);
                sbi.AddParameter("AskOpen", DbType.String);
                sbi.AddParameter("AskHigh", DbType.String);
                sbi.AddParameter("AskLow", DbType.String);
                sbi.AddParameter("AskClose", DbType.String);
                sbi.AddParameter("Volume", DbType.String);

                for (int i = 0; i < prices.Count; i++)
                {
                    index++;
                    sbi.Insert(new object[] {prices[i].StartTime.Ticks,
                                             prices[i].StartTime.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                                             prices[i].BidOpen,
                                             prices[i].BidHigh,
                                             prices[i].BidLow,
                                             prices[i].BidClose,
                                             prices[i].AskOpen,
                                             prices[i].AskHigh,
                                             prices[i].AskLow,
                                             prices[i].AskClose,
                                             prices[i].Volume});
                }
                sbi.Flush();
            }
        }
        
        /// <summary>
        /// Remove all entries in database
        /// </summary>
        public void removeAllEntries()
        {
            lock (databaseLock)
            {
                index = 0;
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FXCM\\HDD\\Data.sqlite"))
                {
                    try
                    {
                        sqlCommand = sqlConn.CreateCommand();
                        sqlCommand.CommandText = "DELETE FROM PriceTable;";
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Fetch data from database
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Data> getEntries(string type)
        {
            lock (databaseLock)
            {
                Console.WriteLine("Requesting: " + type + " from database. ");
                if (type.Equals("Bid"))
                    //SELECT * FROM pricetable WHERE Inx < 1500 AND Inx > 1400 ORDER BY Inx DESC 
                    sqlAdapter = new SQLiteDataAdapter("SELECT distinct(Date), BidOpen, BidHigh, BidLow, BidClose, Volume FROM pricetable" + " ORDER BY DATE ASC;", sqlConn);
                else if (type.Equals("Ask"))
                    sqlAdapter = new SQLiteDataAdapter("SELECT distinct(Date), AskOpen, AskHigh, AskLow, AskClose, Volume FROM pricetable" + " ORDER BY DATE ASC;", sqlConn);
                else
                    sqlAdapter = new SQLiteDataAdapter("SELECT distinct(Date), BidOpen, BidHigh, BidLow, BidClose, AskOpen, AskHigh, AskLow, AskClose, Volume FROM pricetable" + " ORDER BY DATE ASC;", sqlConn);
                dataSet = new DataSet();
                sqlAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
                List<Data> priceList = new List<Data>();
                foreach (DataRow dr in dataTable.Rows)
                {
                    //Console.WriteLine(Convert.ToInt64(dr[0]) + " " + Convert.ToDouble(dr[1]) + " " + Convert.ToDouble(dr[2]));
                    DateTime Date = new DateTime(Convert.ToInt64(dr[0]));
                    double bidopen = 0, bidhigh = 0, bidlow = 0, bidclose = 0, askopen = 0, askhigh = 0, asklow = 0, askclose = 0;
                    int volume = 0;
                    if (type.Equals("Bid"))
                    {
                        bidopen = Convert.ToDouble(dr[1]);
                        bidhigh = Convert.ToDouble(dr[2]);
                        bidlow = Convert.ToDouble(dr[3]);
                        bidclose = Convert.ToDouble(dr[4]);
                        volume = Convert.ToInt32(dr[5]);
                    }
                    else if (type.Equals("Ask"))
                    {
                        askopen = Convert.ToDouble(dr[1]);
                        askhigh = Convert.ToDouble(dr[2]);
                        asklow = Convert.ToDouble(dr[3]);
                        askclose = Convert.ToDouble(dr[4]);
                        volume = Convert.ToInt32(dr[5]);
                    }
                    else
                    {
                        bidopen = Convert.ToDouble(dr[1]);
                        bidhigh = Convert.ToDouble(dr[2]);
                        bidlow = Convert.ToDouble(dr[3]);
                        bidclose = Convert.ToDouble(dr[4]);
                        askopen = Convert.ToDouble(dr[5]);
                        askhigh = Convert.ToDouble(dr[6]);
                        asklow = Convert.ToDouble(dr[7]);
                        askclose = Convert.ToDouble(dr[8]);
                        volume = Convert.ToInt32(dr[9]);
                    }
                    priceList.Add(new Data(askopen, askhigh, asklow, askclose, bidopen, bidhigh, bidlow, bidclose, Date, volume));
                }
                if (priceList.Count > 0)
                    Console.WriteLine("Rows returned: " + dataTable.Rows.Count + ". First Row returned: " + priceList[0].StartTime + " Last Row returned: " + priceList[priceList.Count - 1].StartTime);
                return priceList;
            }
        }
        
        /// <summary>
        /// Get Number of rows
        /// </summary>
        /// <returns></returns>
        public long getNumberOfRows()
        {
            try
            {

                sqlCommand = sqlConn.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) AS Inx FROM PriceTable;";
                long count = (Int64)sqlCommand.ExecuteScalar();
                return count;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        /// <summary>
        /// Get Max date
        /// </summary>
        /// <returns></returns>
        public DateTime getMaxDate()
        {

            string maxDate = "";

            try
            {

                sqlCommand = sqlConn.CreateCommand();
                sqlCommand.CommandText = "SELECT MAX(DT) FROM PRICETABLE";
                maxDate = (string)sqlCommand.ExecuteScalar();
                return Convert.ToDateTime(maxDate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Convert.ToDateTime(maxDate);

        }
    }
}