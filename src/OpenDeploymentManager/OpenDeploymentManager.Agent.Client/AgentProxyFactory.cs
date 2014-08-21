using System;
using Castle.DynamicProxy;

namespace OpenDeploymentManager.Agent.Client
{
    public class AgentProxyFactory : IAgentProxyFactory
    {
        private readonly ProxyGenerator generator = new ProxyGenerator();

        #region Implementation of IAgentProxyFactory

        public T CreateProxy<T>(Uri uri) where T : class
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            var interceptor = new WcfClientProxyInterceptor<T>(uri);

            return this.generator.CreateInterfaceProxyWithoutTarget<T>(interceptor);
        }

        #endregion
    }
}