namespace OpenDeploymentManager.Server.Contracts
{
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        public HttpDeleteAttribute()
            : base("Delete")
        {
        }
    }
}