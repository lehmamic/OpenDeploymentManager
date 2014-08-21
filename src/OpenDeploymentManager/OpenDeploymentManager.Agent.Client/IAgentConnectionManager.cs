using System.Collections.Generic;

namespace OpenDeploymentManager.Agent.Client
{
    public interface IAgentConnectionManager
    {
        IEnumerable<IDeploymentAgent> Discover();
    }
}