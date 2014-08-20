using System.ServiceModel;

namespace OpenDeploymentManager.Agent.Contracts
{
    [ServiceContract]
    [ServiceRoute("deployment")]
    public interface IDeploymentService
    {
    }
}
