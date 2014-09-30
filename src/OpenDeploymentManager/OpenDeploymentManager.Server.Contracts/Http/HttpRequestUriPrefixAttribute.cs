using System;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class HttpRequestUriPrefixAttribute : Attribute
    {
        private readonly string routePrefix;

        public HttpRequestUriPrefixAttribute(string routePrefix)
        {
            if (routePrefix == null)
            {
                throw new ArgumentNullException("route");
            }

            this.routePrefix = routePrefix;
        }

        public string RoutePrefix
        {
            get
            {
                return this.routePrefix;
            }
        }
    }
}