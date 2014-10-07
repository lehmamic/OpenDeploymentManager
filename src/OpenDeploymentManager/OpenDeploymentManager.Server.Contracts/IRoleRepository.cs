using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [HttpRequestUriPrefix("api/Roles")]
    public interface IRoleRepository
    {
        [HttpRequestUri("")]
        PagingResult<Role> Query(PagingQuery<Role> query);

        [HttpRequestUri("{id}")]
        Role GetById(string id);
    }
}
