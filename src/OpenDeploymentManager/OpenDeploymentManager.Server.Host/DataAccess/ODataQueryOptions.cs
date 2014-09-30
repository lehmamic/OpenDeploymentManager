using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    [ODataQueryParameterBinding]
    public class ODataQueryOptions<TDto, TEntity> : ODataQueryOptions<TEntity>
    {
        public ODataQueryOptions(ODataQueryContext context, HttpRequestMessage request)
            : base(context, request.Map<TDto, TEntity>())
        {
        }
    }
}