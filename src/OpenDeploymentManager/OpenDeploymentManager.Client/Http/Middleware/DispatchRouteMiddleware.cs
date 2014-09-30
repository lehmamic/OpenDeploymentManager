using System;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class DispatchRouteMiddleware : IMiddleware
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

            context.Request.RequestUri = context.Method.GetRoute();

            return next.Invoke(context);
        }
        #endregion
    }
}