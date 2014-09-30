using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [HttpRequestUriPrefix("api/Users")]
    public interface IUserRepository
    {
        [HttpRequestUri("")]
        PagingResult<User> Query(PagingQuery<User> query);

        [HttpRequestUri("{id}")]
        User GetUserById(string id);

        [HttpRequestUri("")]
        User CreateUser([HttpBodyParameter] CreateUser user);

        [HttpRequestUri("{id}")]
        void UpdateUser(string id, [HttpBodyParameter] User user);

        [HttpRequestUri("{id}")]
        void DeleteUser(string id);

        [HttpRequestUri("{id}/SetPassword")]
        [HttpPostContract]
        void SetPassword(string id, string password, string confirmPassword);
    }
}
