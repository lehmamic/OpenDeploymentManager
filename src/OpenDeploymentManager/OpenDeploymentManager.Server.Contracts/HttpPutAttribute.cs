namespace OpenDeploymentManager.Server.Contracts
{
    public class HttpPutAttribute : HttpMethodAttribute
    {
        public HttpPutAttribute()
            : base("Put")
        {
        }
    }
}