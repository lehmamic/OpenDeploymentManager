using System.Net.Http;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal interface IHttpResponseContext
    {
        HttpResponseMessage Response { get; }

        object ReturnValue { get; set; }
    }
}