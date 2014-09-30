using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IAccountRepository
    {
        [HttpRequestContract("Account")]
        User GetUser();

        [HttpRequestContract("Account")]
        [HttpPostContract]
        void ChangePassword(string password, string confirmPassword);
    }
}