using OpenDeploymentManager.Agent.Host;

namespace OpenDeploymentManager.Agent.IntegrationTests
{
    public class AgentHost : Program
    {
        public void StartAgent()
        {
            this.OnStart(new string[0]);
        }

        public void StopAgent()
        {
            this.StopAgent();
        }
    }
}