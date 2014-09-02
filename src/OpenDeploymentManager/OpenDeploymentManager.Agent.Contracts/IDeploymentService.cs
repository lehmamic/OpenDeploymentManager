using System;
using System.ServiceModel;

namespace OpenDeploymentManager.Agent.Contracts
{
    [ServiceContract]
    [ServiceRoute("deployment")]
    public interface IDeploymentService
    {
        [OperationContract]
        void Deploy(Guid deploymentId, Guid deploymentNodeId, string template, KeyValue<object>[] arguments, KeyValue<string>[] variables);
    }
}
