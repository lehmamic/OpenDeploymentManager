using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [ODataQueryParameterBinding]
    public class PagingQuery<T> : IPagingQuery
    {
        public int Top { get; set; }

        public int Skip { get; set; }

        public InlineCountOptions InlineCount { get; set; }
    }
}