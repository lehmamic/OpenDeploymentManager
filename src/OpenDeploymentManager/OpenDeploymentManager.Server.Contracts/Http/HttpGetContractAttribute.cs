namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class HttpGetContractContractAttribute : HttpMethodContractAttribute
    {
        public HttpGetContractContractAttribute()
            : base("Get")
        {
        }
    }
}