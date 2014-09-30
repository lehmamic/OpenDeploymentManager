using System;
using System.Globalization;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class DispatchRouteMiddleware : IMiddleware
    {
        private const string RootIndicator = "~/";

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

            string routePrefix = context.Method.DeclaringType.GetRoutePrefix();
            string route = context.Method.GetRoute();

            string requestUri = route.StartsWith(RootIndicator) ? route.Substring(RootIndicator.Length) : string.Format(CultureInfo.InvariantCulture, "{0}/{1}", routePrefix, route);
            context.Request.RequestUri = new Uri(requestUri, UriKind.Relative);

            return next.Invoke(context);
        }
        #endregion
    }
}