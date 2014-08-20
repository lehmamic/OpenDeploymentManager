using System;

namespace OpenDeploymentManager.Agent.Contracts
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ServiceRouteAttribute : Attribute
    {
        private readonly string route;

        public ServiceRouteAttribute(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                throw new ArgumentException("Teh service rout is null or empty.", "route");
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
