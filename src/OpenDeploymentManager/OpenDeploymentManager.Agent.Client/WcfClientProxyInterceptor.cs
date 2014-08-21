using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.ServiceModel;
using Castle.DynamicProxy;
using PiranhaDeploy.Agent.Contracts;

namespace OpenDeploymentManager.Agent.Client
{
    public class WcfClientProxyInterceptor<T> : IInterceptor where T : class
    {
        private readonly Uri uri;
        private readonly ChannelFactory<T> factory = new ChannelFactory<T>(new NetTcpBinding());

        public WcfClientProxyInterceptor(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            this.uri = uri;
        }

        #region Implementation of IInterceptor

        [DebuggerStepThrough]
        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException("invocation");
            }

            var serviceUri = new Uri(this.uri, typeof(T).GetServiceRoute());
            var endpoint = new EndpointAddress(serviceUri);

            var channel = (ICommunicationObject)this.factory.CreateChannel(endpoint);
            try
            {
                if (channel != null)
                {
                    invocation.ReturnValue = invocation.Method.Invoke(channel, invocation.Arguments);
                    channel.Close();
                }
            }
            catch (TargetInvocationException exception)
            {
                if (channel != null)
                {
                    channel.Abort();
                }

                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }
            catch
            {
                if (channel != null)
                {
                    channel.Abort();
                }

                throw;
            }
        }

        #endregion
    }
}