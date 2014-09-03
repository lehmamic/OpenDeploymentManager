using System;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.Practices.Unity;
using NLog;
using OpenDeploymentManager.Agent.Contracts;
using OpenDeploymentManager.Agent.Host.Properties;
using OpenDeploymentManager.Agent.Host.Services;
using PiranhaDeploy.Agent.Contracts;
using Unity.Wcf;

namespace OpenDeploymentManager.Agent.Host.StartupTasks
{
    public class InitializeWcfServiceHostsTask : IStartupTask, IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static readonly Uri ServiceHostBaseAddress = new Uri(AgentConfiguration.AgentUrl);

        private readonly IUnityContainer container;

        private bool disposed = false;
        private ServiceHost serviceHost;

        public InitializeWcfServiceHostsTask(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        ~InitializeWcfServiceHostsTask()
        {
            this.Dispose(false);
        }

        #region Implementation of IStartupTask
        public void Run()
        {
            Log.Trace(Resources.InitializeWcfServiceHostsTask_InitializingServiceHost);

            // Create a ServiceHost for the CalculatorService type.
            this.serviceHost = new UnityServiceHost(this.container, typeof(DeploymentAgentService), ServiceHostBaseAddress);

            this.serviceHost.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
            this.serviceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());

            this.serviceHost.AddServiceEndpoint(typeof(IAgentInfoService), new NetTcpBinding(), typeof(IAgentInfoService).GetServiceRoute());
            this.serviceHost.AddServiceEndpoint(typeof(IDeploymentService), new NetTcpBinding(), typeof(IDeploymentService).GetServiceRoute());

            this.serviceHost.Open();

            Log.Info(CultureInfo.InvariantCulture, Resources.InitializeWcfServiceHosts_AgentIsListeningOnPort, ServiceHostBaseAddress.AbsoluteUri);
        }

        public void Reset()
        {
            this.CleanupWcfHosts();
        }
        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.CleanupWcfHosts();
                }

                this.disposed = true;
            }
        }

        private void CleanupWcfHosts()
        {
            if (this.serviceHost != null)
            {
                this.serviceHost.Close();
                this.serviceHost = null;
            }
        }
    }
}
