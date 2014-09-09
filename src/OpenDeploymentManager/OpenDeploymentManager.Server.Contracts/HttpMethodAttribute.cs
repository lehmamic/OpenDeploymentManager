using System;

namespace OpenDeploymentManager.Server.Contracts
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class HttpMethodAttribute : Attribute
    {
        private readonly string method;

        protected HttpMethodAttribute(string method)
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