using System;

namespace OpenDeploymentManager.Client
{
    public class OpenDeploymentManagerClient : IOpenDeploymentManagerClient
    {
        private readonly OpenDeploymentManagerEndpoint serverEndpoint;

        public OpenDeploymentManagerClient(OpenDeploymentManagerEndpoint serverEndpoint)
        {
            if (serverEndpoint == null)
            {
                throw new ArgumentNullException("serverEndpoint");
            }

            this.serverEndpoint = serverEndpoint;
        }

        public TService GetService<TService>()
        {
            return default(TService);
        }
    }
}