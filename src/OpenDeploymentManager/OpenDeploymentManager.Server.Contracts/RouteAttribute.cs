using System;

namespace OpenDeploymentManager.Server.Contracts
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RouteAttribute : Attribute
    {
        private readonly string route;

        public RouteAttribute(string route)
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
