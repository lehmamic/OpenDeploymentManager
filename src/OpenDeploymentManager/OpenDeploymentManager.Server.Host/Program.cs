using System;
using System.ServiceProcess;
using Bootstrap;
using Bootstrap.AutoMapper;
using Bootstrap.Extensions.StartupTasks;
using Bootstrap.Unity;
using NLog;
using OpenDeploymentManager.Server.Host.Properties;
using OpenDeploymentManager.Server.Host.StartupTasks;

namespace OpenDeploymentManager.Server.Host
{
    public class Program : ServiceBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The lifetime of the service is handled by the ServiceBase class.")]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += Program.HanldeException;

            var service = new Program();
            if (Environment.UserInteractive)
            {
                service.OnStart(args);

                Console.WriteLine(Resources.Program_PressAnyKeyToExit);
                Console.Read();

                service.OnStop();
            }
            else
            {
                ServiceBase.Run(service);
            }
        }

        #region Override ServiceBase
        protected override void OnStart(string[] args)
        {
            Log.Debug(Resources.Program_StartingAgent);

            base.OnStart(args);

            Bootstrapper
                .With.Unity()
                .And.AutoMapper()
                .And.StartupTasks().UsingThisExecutionOrder(c =>
                    c.First<InitializeOwinTask>())
                .Start();

            Log.Info(Resources.Program_AgentStarted);
        }

        protected override void OnStop()
        {
            Log.Debug(Resources.Program_StoppingAgent);
            base.OnStop();

            Bootstrapper.Reset();

            Log.Info(Resources.Program_AgentStopped);
        }
        #endregion

        private static void HanldeException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Error(Resources.Program_UnhanldedException, (Exception)e.ExceptionObject);
        }
    }
}
