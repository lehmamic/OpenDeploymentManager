using System;
using Castle.DynamicProxy;
using OpenDeploymentManager.Client.Http;

namespace OpenDeploymentManager.Client
{
    public class OpenDeploymentManagerClient : IOpenDeploymentManagerClient
    {
        private readonly OpenDeploymentManagerEndpoint serverEndpoint;
        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        public OpenDeploymentManagerClient(OpenDeploymentManagerEndpoint serverEndpoint)
        {
            if (serverEndpoint == null)
            {
                throw new ArgumentNullException("serverEndpoint");
            }

            this.serverEndpoint = serverEndpoint;
        }

        public TService GetService<TService>() where TService : class
        {
            var interceptor = new WebApiProxyInterceptor<TService>(this.serverEndpoint);
            return this.proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(interceptor);
        }
    }
}