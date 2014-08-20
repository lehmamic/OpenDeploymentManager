using System.Configuration;

namespace OpenDeploymentManager.Agent.Host
{
    public static class ApplicationConfiguration
    {
        public static string AgentUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["OpenDeploymentManager:AgentUrl"];
            }
        }
    }
}