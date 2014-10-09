using System;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [HttpRequestUriPrefix("api/Users")]
    public interface IUserRepository
    {
        [HttpRequestUri("")]
        PagingResult<User> Query(PagingQuery<User> query);

        [HttpRequestUri("{id}")]
        User GetById(Guid id);

        [HttpRequestUri("byname/{userName}")]
        User GetByName(string userName);

        [HttpRequestUri("")]
        User Create([HttpBodyParameter] CreateUser user);

        [HttpRequestUri("{id}")]
        void Update(Guid id, [HttpBodyParameter] User user);

        [HttpRequestUri("{id}")]
        void Delete(Guid id);

        [HttpRequestUri("{id}/SetPassword")]
        [HttpPutContract]
        void SetPassword(Guid id, [HttpBodyParameter] SetPassword password);
    }
}
