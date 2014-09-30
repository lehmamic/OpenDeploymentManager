namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal interface INextInvoker
    {
        IHttpResponseContext Invoke(IHttpRequestContext context);
    }
}