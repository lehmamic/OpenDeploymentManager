using System;
using System.ServiceModel;
using System.Threading;
using OpenDeploymentManager.Agent.Contracts;

namespace OpenDeploymentManager.Agent.Client
{
    public class DeploymentAgent : IDeploymentAgent
    {
        private readonly IAgentProxyFactory proxyFactory;
        private readonly Uri uri;
        private readonly Lazy<AgentInfo> agentInfo;

        public DeploymentAgent(IAgentProxyFactory proxyFactory, Uri uri)
        {
            if (proxyFactory == null)
            {
                throw new ArgumentNullException("proxyFactory");
            }

            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            this.proxyFactory = proxyFactory;
            this.uri = uri;
            this.agentInfo = new Lazy<AgentInfo>(() => this.GetService<IAgentInfoService>().GetInfo(), LazyThreadSafetyMode.PublicationOnly);
        }

        public Uri Uri
        {
            get
            {
                return this.uri;
            }
        }

        public string MachineName
        {
            get
            {
                return this.agentInfo.Value.MachineName;
            }
        }

        public Version Version
        {
            get
            {
                return new Version(this.agentInfo.Value.Version);
            }
        }

        public bool IsAlive()
        {
            try
            {
                return this.GetService<IAgentInfoService>().GetInfo() != null;
            }
            catch (CommunicationException)
            {
                return false;
            }
        }

        public T GetService<T>() where T : class
        {
            return this.proxyFactory.CreateProxy<T>(this.uri);
        }
    }
}