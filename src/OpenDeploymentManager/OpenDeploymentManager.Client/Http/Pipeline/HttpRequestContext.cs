using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal class HttpRequestContext : IHttpRequestContext
    {
        private readonly MethodInfo method;
        private readonly HttpParameterDescriptor[] parameters;
        private readonly HttpRequestMessage request;
        private readonly OpenDeploymentManagerEndpoint endpoint;

        public HttpRequestContext(MethodInfo method, object[] arguments, OpenDeploymentManagerEndpoint endpoint)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            this.method = method;
            this.parameters = GetParameterDescriptors(method, arguments).ToArray();
            this.endpoint = endpoint;
            this.request = new HttpRequestMessage();
        }

        public MethodInfo Method
        {
            get
            {
                return this.method;
            }
        }

        public HttpParameterDescriptor[] Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public HttpRequestMessage Request
        {
            get
            {
                return this.request;
            }
        }

        public OpenDeploymentManagerEndpoint Endpoint
        {
            get
            {
                return this.endpoint;
            }
        }

        private static IEnumerable<HttpParameterDescriptor> GetParameterDescriptors(MethodInfo methodInfo, object[] parameterValues)
        {
            ParameterInfo[] parameters = methodInfo.GetParameters();

            for (int i = 0; i < parameters.Length; i++)
            {
                yield return new HttpParameterDescriptor(parameters[i], parameterValues[i]);
            }
        }
    }
}