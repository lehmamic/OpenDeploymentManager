using System;
using System.Linq;
using System.ServiceModel;
using DevTrends.WCFDataAnnotations;
using OpenDeploymentManager.Agent.Contracts;
using OpenDeploymentManager.Agent.Host.Logging;
using OpenDeploymentManager.Deployment;

namespace OpenDeploymentManager.Agent.Host.Services
{
    [ValidateDataAnnotationsBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class DeploymentAgentService : IAgentInfoService, IDeploymentService
    {
        private readonly IDeploymentManager deploymentManager;

        public DeploymentAgentService(IDeploymentManager deploymentManager)
        {
            if (deploymentManager == null)
            {
                throw new ArgumentNullException("deploymentManager");
            }

            this.deploymentManager = deploymentManager;
        }

        #region Implementation of IAgentStatusService
        [LoggingCallHandler]
        public AgentInfo GetInfo()
        {
            return new AgentInfo
            {
                MachineName = Environment.MachineName,
                Version = this.GetType().Assembly.GetName().Version.ToString()
            };
        }
        #endregion

        #region Implementation of IAgentStatusService
        [LoggingCallHandler]
        public void Deploy(Guid deploymentId, Guid deploymentNodeId, string template, KeyValue<object>[] arguments, KeyValue<string>[] variables)
        {
            this.deploymentManager.RunDeployment(
                template,
                arguments.ToDictionary(i => i.Key, i => i.Value),
                variables.ToDictionary(i => i.Key, i => i.Value));
        }
        #endregion
    }
}
