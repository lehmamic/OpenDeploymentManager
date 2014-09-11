namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class HttpPutContractAttribute : HttpMethodContractAttribute
    {
        public HttpPutContractAttribute()
            : base("Put")
        {
        }
    }
}