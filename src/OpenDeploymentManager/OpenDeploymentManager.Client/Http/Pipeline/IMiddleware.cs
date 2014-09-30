namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal interface IMiddleware
    {
        IHttpResponseContext Invoke(IHttpRequestContext context, INextInvoker next);
    }
}