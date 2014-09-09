namespace OpenDeploymentManager.Server.Contracts
{
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
            : base("Post")
        {
        }
    }
}