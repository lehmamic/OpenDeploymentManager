using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Castle.DynamicProxy;

namespace OpenDeploymentManager.Client
{
    internal class WebApiProxyInterceptor<T> : IInterceptor where T : class
    {
        private readonly OpenDeploymentManagerEndpoint endpoint;
        ////private readonly HttpClient httpClient = new H$


        public WebApiProxyInterceptor(OpenDeploymentManagerEndpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        #region Implementation of IInterceptor

        [DebuggerStepThrough]
        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException("invocation");
            }

            using (HttpClient client = HttpClientFactory.Create())
            {
                client.BaseAddress = this.endpoint.UriResolver.BaseUri;

                AuthenticationHeaderValue authHeader = endpoint.Authentication.Authenticate(this.endpoint.UriResolver);
                client.DefaultRequestHeaders.Add(authHeader.Header, authHeader.Value);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json; charset=utf-8"));

                Uri requestUri = invocation.Method.GetRoute();
                var request = new HttpRequestMessage(invocation.Method.GetHttpMethod(), requestUri);

                var resourceParameter = invocation.Method.GetParameters().FirstOrDefault(p => p.ParameterType == typeof(T));
                if (resourceParameter != null)
                {
                    int index = invocation.Method.GetParameters().ToList().IndexOf(resourceParameter);
                    request.Content = new ObjectContent(typeof(T), invocation.Arguments[index], new JsonMediaTypeFormatter());
                }

                HttpResponseMessage response = client.SendAsync(request).WaitOn();
                if (invocation.Method.ReturnType != typeof(void))
                {
                    invocation.ReturnValue = response.Content.ReadAsAsync(invocation.Method.ReturnType).WaitOn();
                }
            }
            ////    }
            ////}
            ////catch (TargetInvocationException exception)
            ////{
            ////    if (channel != null)
            ////    {
            ////        channel.Abort();
            ////    }

            ////    ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            ////}
            ////catch
            ////{
            ////    if (channel != null)
            ////    {
            ////        channel.Abort();
            ////    }

            ////    throw;
            ////}
        }

        #endregion
    }
}