using System;
using System.Web.Http;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host
{
    public static class GlobalConfiguration
    {
        private static HttpConfiguration httpConfiguration;

        public static HttpConfiguration Configuration
        {
            get
            {
                if (httpConfiguration == null)
                {
                    httpConfiguration = new HttpConfiguration();
                }

                return httpConfiguration;
            }
        }

        public static void Configure(Action<HttpConfiguration> configurationCallback)
        {
            configurationCallback.ArgumentNotNull("configurationCallback");

            configurationCallback(Configuration);
        }
    }
}