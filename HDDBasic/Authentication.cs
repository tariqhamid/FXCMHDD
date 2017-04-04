using System;
using System.Text;
using System.IO;
using System.Net;
using NLog;

namespace HDD
{
    public class Authentication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Authentication()
        {
            //Log.logMessageToFile("Authentication","Authentication Declared");
            logger.Debug("Authentication Declared");
        }

        public static string getResponse(string acctNo)
        {
            //Log.logMessageToFile("Authentication", "Attempting autentication to: " + acctNo);\
            logger.Debug("Attempting autentication to: {0}", acctNo);
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            string address = "http://psa.fxcorporate.com/index.php?m=authenticate&acct=" + acctNo;
            HttpWebRequest request;
            HttpWebResponse response;
            Stream resStream = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(address);
                response = (HttpWebResponse)request.GetResponse();
                resStream = response.GetResponseStream();
            }
            catch (Exception e)
            {
                //Log.logMessageToFile("Authentication", e.Message);
                //Log.logMessageToFile("Authentication", e.StackTrace);
                logger.Debug(e.Message);
                return "Cannot authenticate. Please check your internet connection.";
            }

            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.ASCII.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0);
            //Log.logMessageToFile("Authentication", "Message returned: " + sb.ToString());
            logger.Debug("Message returned: {0}", sb.ToString());
            if (sb.ToString().Equals("0"))
            {
                return "success";
            }
            else
            {
                return "You have either entered an incorrect account number or your subscription has expired.";
            }
        }
    }
}
