using System;
using System.Net.Http;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    public abstract class HttpParameterBindingAttribute : Attribute
    {
        public abstract void BindParameter(HttpParameterDescriptor parameter, HttpRequestMessage request);
    }
}