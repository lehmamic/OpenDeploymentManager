using System;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class OperationContractAttribute : Attribute
    {
        private readonly string route;

        public OperationContractAttribute(string route)
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
