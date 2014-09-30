using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [ODataQueryParameterBinding]
    public class PagingQuery
    {
        public int Top { get; set; }
        
        public int Skip { get; set; }
    }
}