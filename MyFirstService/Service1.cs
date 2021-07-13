using NLog;
using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace MyFirstService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        Logger loggerx = LogManager.GetCurrentClassLogger();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WriteToFile("Service is started at " + DateTime.Now);
                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                timer.Interval = 5000;
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                loggerx.Error(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                WriteToFile("Service is stopped at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                loggerx.Error(ex);
            }
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {   try
            {
                WriteToFile("Service is recall at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                loggerx.Error(ex);
            }
        }
        public void WriteToFile(string Message)
        { try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                loggerx.Error(ex);
            }
        }

    }
}
