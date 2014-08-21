using System;

namespace OpenDeploymentManager.Agent.Client
{
    public interface IAgentProxyFactory
    {
        T CreateProxy<T>(Uri address) where T : class;
    }
}