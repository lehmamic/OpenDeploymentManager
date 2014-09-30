using System;
using System.Net;
using System.Net.Http;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Client.Http.Pipeline;
using OpenDeploymentManager.Server.Contracts;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        #region Implementation of IMiddleware
        public IHttpResponseContext Invoke(IHttpRequestContext context, INextInvoker next)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            IHttpResponseContext response = next.Invoke(context);

            if (response.Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResult = response.Response.Content.ReadAsAsync<ErrorResult>().WaitOn();
                throw new ValidationException(errorResult.Message, errorResult);
            }
            else if (response.Response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new SecurityException("Authorization failed.", (int)response.Response.StatusCode);
            }
            else if (response.Response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new SecurityException("No access to the requested resource.", (int)response.Response.StatusCode);
            }
            else if (!response.Response.IsSuccessStatusCode)
            {
                var waitOn = response.Response.Content.ReadAsStringAsync().WaitOn();
                throw new ServerException(waitOn, (int)response.Response.StatusCode);
            }

            return response;
        }
        #endregion
    }
}
