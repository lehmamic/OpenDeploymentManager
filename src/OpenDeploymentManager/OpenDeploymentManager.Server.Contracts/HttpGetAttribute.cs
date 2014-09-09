namespace OpenDeploymentManager.Server.Contracts
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
            : base("Get")
        {
        }
    }
}