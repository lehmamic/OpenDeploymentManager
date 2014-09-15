using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IUserRepository
    {
        [OperationContract("Users")]
        PagingResult<User> Query();

        [OperationContract("Users/{id}")]
        User GetUserById(string id);

        [OperationContract("Users")]
        User CreateUser([HttpBodyContentAttribute]CreateUser user);

        [OperationContract("Users/{id}")]
        void UpdateUser(User user);

        [OperationContract("Users/{id}")]
        void DeleteUser(string id);

        [OperationContract("Users/{id}/SetPassword")]
        [HttpPostContract]
        void SetPassword(string id, string password, string confirmPassword);
    }
}
