using System;
using System.ServiceModel;
using OpenDeploymentManager.Agent.Contracts;

namespace OpenDeploymentManager.Agent.Host.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class DeploymentAgentService : IAgentInfoService, IDeploymentService
    {
        #region Implementation of IAgentStatusService
        public AgentInfo GetInfo()
        {
            return new AgentInfo
            {
                MachineName = Environment.MachineName,
                Version = this.GetType().Assembly.GetName().Version.ToString()
            };
        }
        #endregion
    }
}
