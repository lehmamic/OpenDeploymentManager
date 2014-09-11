using System;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class HttpMethodContractAttribute : Attribute
    {
        private readonly string method;

        protected HttpMethodContractAttribute(string method)
        {
            this.method = method;
        }

        public string Method
        {
            get
            {
                return this.method;
            }
        }
    }
}