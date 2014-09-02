using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using OpenDeploymentManager.Agent.Contracts;

namespace OpenDeploymentManager.Agent.Client
{
    public class AgentConnectionManager : IAgentConnectionManager
    {
        private readonly IAgentProxyFactory proxyFactory;

        public AgentConnectionManager()
            : this(new AgentProxyFactory())
        {
        }

        public AgentConnectionManager(IAgentProxyFactory proxyFactory)
        {
            if (proxyFactory == null)
            {
                throw new ArgumentNullException("proxyFactory");
            }

            this.proxyFactory = proxyFactory;
        }

        public IEnumerable<IDeploymentAgent> Discover()
        {
            using (var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint()))
            {
                FindResponse result = discoveryClient.Find(new FindCriteria(typeof(IAgentInfoService)));
                return result.Endpoints.Select(e => new DeploymentAgent(this.proxyFactory, e.Address.Uri.BaseAddress<IAgentInfoService>()));
            }
        }

        public IDeploymentAgent Discover(Uri agentUri)
        {
            if (agentUri == null)
            {
                throw new ArgumentNullException("agentUri");
            }

            var agent = new DeploymentAgent(this.proxyFactory, agentUri);
            if (!agent.IsAlive())
            {
                string message = string.Format(CultureInfo.InvariantCulture, "The agent with the uri {0} has not been found.", agentUri);
                throw new CommunicationException(message);
            }

            return agent;
        }
    }
}