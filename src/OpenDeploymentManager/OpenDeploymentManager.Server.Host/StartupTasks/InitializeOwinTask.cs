using System;
using System.Globalization;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using NLog;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Properties;
using Owin;

namespace OpenDeploymentManager.Server.Host.StartupTasks
{
    public class InitializeOwinTask : IStartupTask, IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IUnityContainer container;

        private bool disposed;
        private IDisposable webApp;

        public InitializeOwinTask(IUnityContainer container)
        {
            this.container = container.ArgumentNotNull("container");
        }

        ~InitializeOwinTask()
        {
            this.Dispose(false);
        }

        #region Implementation of IStartupTask
        public void Run()
        {
            var options = new StartOptions();
            options.Urls.Add(ServerConfiguration.ServerUrl);

            this.webApp = WebApp.Start(options, this.BuildWebApp);
            
            Log.Info(CultureInfo.InvariantCulture, Resources.InitializeOwinTask_OwinIsListeningOnPort, ServerConfiguration.ServerUrl);
        }

        public void Reset()
        {
            this.CleanupWebApp();
        }
        #endregion

        #region Implementation of IDisposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private void BuildWebApp(IAppBuilder builder)
        {
            Startup.Configuration(builder, container);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.CleanupWebApp();
                }

                this.disposed = true;
            }
        }

        private void CleanupWebApp()
        {
            if (this.webApp != null)
            {
                this.webApp.Dispose();
                this.webApp = null;
            }
        }
    }
}
