using System.ServiceModel;

namespace OpenDeploymentManager.Agent.Contracts
{
    [ServiceContract]
    [ServiceRoute("info")]
    public interface IAgentInfoService
    {
        [OperationContract]
        AgentInfo GetInfo();
    }
}