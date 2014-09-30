namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal interface IHttpRequestBuilder
    {
        IHttpRequestBuilder Use(IMiddleware middleware);
    }
}