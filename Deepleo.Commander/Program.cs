using Deepleo.Commander.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Deepleo.Commander
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            log4net.Config.XmlConfigurator.Configure(new FileInfo("LogWriterConfig.xml"));
            AppLogManager.Write("Deepleo Commander v1.0");
            AppLogManager.Write("Process ID:" + Process.GetCurrentProcess().Id);
            AppLogManager.Write("Start Time:" + DateTime.Now.ToString());
            AppLogManager.Write("---------------------------------------------------");
            var ServiceHost = new ServiceHost(typeof(CommanderService));
            ServiceHost.Opened += delegate
            {
                AppLogManager.Write("CommanderService Opened Successfully.");
                AppLogManager.Write("Commander base url :");
                foreach (var add in ServiceHost.BaseAddresses)
                {
                    AppLogManager.Write(add.AbsoluteUri);
                }
                AppLogManager.Write("====================================================");
            };
            if (ServiceHost.State != CommunicationState.Opened || ServiceHost.State != CommunicationState.Opening)
            {
                ServiceHost.Open();
            }
            Console.ReadKey();
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = (Exception)e.ExceptionObject;
            AppLogManager.Write("UnhandledException caught : " + error.Message);
        }
    }
}
