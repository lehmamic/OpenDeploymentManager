using System;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HttpRequestUriAttribute : Attribute
    {
        private readonly string route;

        public HttpRequestUriAttribute(string route)
        {
            if (route == null)
            {
                throw new ArgumentNullException("route");
            }

            this.route = route;
        }

        public string Route
        {
            get
            {
                return this.route;
            }
        }
    }
}
