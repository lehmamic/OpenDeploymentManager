using System;
using System.Net.Http;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class DispatchReturnValueMiddleware : IMiddleware
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
            if (context.Method.ReturnType != typeof(void))
            {
                response.ReturnValue = response.Response.Content.ReadAsAsync(context.Method.ReturnType).WaitOn();
            }

            return response;
        }
        #endregion
    }
}