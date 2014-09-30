using System;
using OpenDeploymentManager.Client.Http.Pipeline;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal static class MiddlewareExtensions
    {
        public static IHttpRequestBuilder Use<TMiddleware>(this IHttpRequestBuilder builder) where TMiddleware : IMiddleware,  new()
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            return builder.Use(new TMiddleware());
        }
    }
}
