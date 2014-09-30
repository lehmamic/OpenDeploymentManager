using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IAccountRepository
    {
        [HttpRequestUri("Account")]
        User GetUser();

        [HttpRequestUri("Account")]
        [HttpPostContract]
        void ChangePassword(string password, string confirmPassword);
    }
}