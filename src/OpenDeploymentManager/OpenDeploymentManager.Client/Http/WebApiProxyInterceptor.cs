using System;
using Castle.DynamicProxy;
using OpenDeploymentManager.Client.Http.Middleware;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http
{
    internal class WebApiProxyInterceptor<T> : IInterceptor where T : class
    {
        private readonly OpenDeploymentManagerEndpoint endpoint;

        public WebApiProxyInterceptor(OpenDeploymentManagerEndpoint endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            this.endpoint = endpoint;
        }

        #region Implementation of IInterceptor
        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException("invocation");
            }

            Uri baseAddress = this.endpoint.UriResolver.Resolve("~/api/");
            using (var request = new HttpRequestInvoker(baseAddress))
            {
                request.Configure(builder =>
                        {
                            builder.Use<DispatchRouteMiddleware>();
                            builder.Use<DispatchHttpMethodMiddleware>();
                            builder.Use<DispatchHttpRequestParameterMiddleware>();
                            builder.Use<AuthenticationMiddleware>();
                            builder.Use<DispatchReturnValueMiddleware>();
                            builder.Use<ErrorHandlerMiddleware>();
                        });

                var context = new HttpRequestContext(invocation.Method, invocation.Arguments, this.endpoint);
                IHttpResponseContext response = request.Execute(context);
                if (invocation.Method.ReturnType != typeof(void))
                {
                    invocation.ReturnValue = response.ReturnValue;
                }
            }
        }
        #endregion
    }
}