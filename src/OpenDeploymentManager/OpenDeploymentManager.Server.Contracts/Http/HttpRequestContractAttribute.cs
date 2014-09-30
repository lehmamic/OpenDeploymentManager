using System;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HttpRequestContractAttribute : Attribute
    {
        private readonly string route;

        public HttpRequestContractAttribute(string route)
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
