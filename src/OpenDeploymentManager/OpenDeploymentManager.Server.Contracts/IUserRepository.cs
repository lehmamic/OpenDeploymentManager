using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IUserRepository
    {
        [HttpRequestContract("Users")]
        PagingResult<User> Query([HttpUrlParameter] PagingQuery query);

        [HttpRequestContract("Users/{id}")]
        User GetUserById(string id);

        [HttpRequestContract("Users")]
        User CreateUser([HttpBodyParameter] CreateUser user);

        [HttpRequestContract("Users/{id}")]
        void UpdateUser(string id, [HttpBodyParameter] User user);

        [HttpRequestContract("Users/{id}")]
        void DeleteUser(string id);

        [HttpRequestContract("Users/{id}/SetPassword")]
        [HttpPostContract]
        void SetPassword(string id, string password, string confirmPassword);
    }
}
