namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class HttpPostContractAttribute : HttpMethodContractAttribute
    {
        public HttpPostContractAttribute()
            : base("Post")
        {
        }
    }
}