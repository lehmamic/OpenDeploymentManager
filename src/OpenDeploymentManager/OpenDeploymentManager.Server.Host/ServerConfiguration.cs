using System.Configuration;

namespace OpenDeploymentManager.Server.Host
{
    public static class ServerConfiguration
    {
        public static string ServerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["OpenDeploymentManager/ServerUrl"];
            }
        }

        public static string UseEmbeddedHttpServer
        {
            get
            {
                return ConfigurationManager.AppSettings["Raven/UseEmbeddedHttpServer"];
            }
        }

        public static string RavenDbPort
        {
            get
            {
                return ConfigurationManager.AppSettings["Raven/Port"];
            }
        }
    }
}