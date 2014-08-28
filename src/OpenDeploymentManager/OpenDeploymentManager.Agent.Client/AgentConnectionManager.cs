using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Discovery;
using OpenDeploymentManager.Agent.Contracts;

namespace OpenDeploymentManager.Agent.Client
{
    public class AgentConnectionManager : IAgentConnectionManager
    {
        private readonly IAgentProxyFactory proxyFactory;

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
    }
}