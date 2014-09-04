using System.Configuration;

namespace OpenDeploymentManager.Server.Host
{
    public static class ServerConfiguration
    {
        public static string AgentUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["OpenDeploymentManager:ServerUrl"];
            }
        }
    }
}