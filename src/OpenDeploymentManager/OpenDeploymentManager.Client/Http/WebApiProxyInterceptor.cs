using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Castle.DynamicProxy;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client.Http
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

        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
            {
                throw new ArgumentNullException("invocation");
            }

            using (HttpClient client = HttpClientFactory.Create())
            {
                client.BaseAddress = this.endpoint.UriResolver.Resolve("~/api/");

                AuthenticationHeaderValue authHeader = this.endpoint.Authentication.Authenticate(this.endpoint.UriResolver);
                client.DefaultRequestHeaders.Add(authHeader.Header, authHeader.Value);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Uri requestUri = invocation.Method.GetRequestUri(invocation.Arguments);
                var request = new HttpRequestMessage(invocation.Method.GetHttpMethod(), requestUri);

                var resourceParameter = invocation.Method.GetParameters().FirstOrDefault(p => p.IsDefined(typeof(HttpBodyContentAttribute), true));
                if (resourceParameter != null)
                {
                    int index = invocation.Method.GetParameters().ToList().IndexOf(resourceParameter);
                    request.Content = new ObjectContent(resourceParameter.ParameterType, invocation.Arguments[index], new JsonMediaTypeFormatter());
                }

                HttpResponseMessage response = client.SendAsync(request).WaitOn();
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = response.Content.ReadAsAsync<string>().WaitOn();
                    throw new ServerException(errorContent, (int)response.StatusCode);
                }

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