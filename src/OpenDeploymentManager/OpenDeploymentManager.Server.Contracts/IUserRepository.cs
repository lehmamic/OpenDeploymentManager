using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [HttpRequestUriPrefix("api/Users")]
    public interface IUserRepository
    {
        [HttpRequestUri("")]
        PagingResult<User> Query(PagingQuery<User> query);

        [HttpRequestUri("{id}")]
        User GetById(string id);

        [HttpRequestUri("")]
        User Create([HttpBodyParameter] CreateUser user);

        [HttpRequestUri("{id}")]
        void Update(string id, [HttpBodyParameter] User user);

        [HttpRequestUri("{id}")]
        void Delete(string id);

        [HttpRequestUri("{id}/SetPassword")]
        [HttpPutContract]
        void SetPassword(string id, [HttpBodyParameter] SetPassword password);
    }
}
