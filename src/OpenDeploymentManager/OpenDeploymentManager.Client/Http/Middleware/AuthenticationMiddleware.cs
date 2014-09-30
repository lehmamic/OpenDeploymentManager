using System;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class AuthenticationMiddleware : IMiddleware
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

            AuthenticationHeaderValue authHeader = context.Endpoint.Authentication.Authenticate(context.Endpoint.UriResolver);
            context.Request.Headers.Add(authHeader.Header, authHeader.Value);

            return next.Invoke(context);
        }
        #endregion
    }
}
