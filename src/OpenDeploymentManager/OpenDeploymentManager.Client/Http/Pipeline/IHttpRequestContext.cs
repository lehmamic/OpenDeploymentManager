using System.Net.Http;
using System.Reflection;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal interface IHttpRequestContext
    {
        MethodInfo Method { get; }

        HttpParameterDescriptor[] Parameters { get; }

        HttpRequestMessage Request { get; }

        OpenDeploymentManagerEndpoint Endpoint { get; }
    }
}