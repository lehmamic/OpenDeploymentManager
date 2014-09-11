using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IAccountRepository
    {
        [OperationContract("Account")]
        User GetUser();

        [OperationContract("Account")]
        [HttpPostContract]
        void ChangePassword(string password, string confirmPassword);
    }
}