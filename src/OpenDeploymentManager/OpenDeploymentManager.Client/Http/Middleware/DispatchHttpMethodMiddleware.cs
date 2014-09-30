using System;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class DispatchHttpMethodMiddleware : IMiddleware
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

            context.Request.Method = context.Method.GetHttpMethod();

            return next.Invoke(context);
        }
        #endregion
    }
}
