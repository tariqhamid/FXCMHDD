using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace HDD
{
    static class Program
    {
        private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b1";
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The code below allows HDD Basic to only have one instance of it running,
        /// It will give an error else wise.
        /// </summary>
        [STAThread]
        static void Main()
        {


            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    logger.Debug("Instance already running");
                    MessageBox.Show("Instance already running");
                    return;
                }

                try {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(LoginForm.getInstance());
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.Message);
                    throw ex;
                }
            }




        }
    }
}
