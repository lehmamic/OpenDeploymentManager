using System.Collections.Generic;
using OpenDeploymentManager.Server.Host.Security;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public interface ISecurityService
    {
        IEnumerable<GlobalResources> GetGlobalResources();

        IEnumerable<ResourceOperations> GetResourceOperations(GlobalResources resource);
    }
}