using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Server.Contracts
{
    [HttpRequestUriPrefix("api/Account")]
    public interface IAccountRepository
    {
        [HttpRequestUri("UserInfo")]
        User Get();

        [HttpRequestUri("UserInfo")]
        void Update(User user);

        [HttpRequestUri("ChangePassword")]
        [HttpPostContract]
        void ChangePassword(ChangePassword password);

        [HttpRequestUri("Logout")]
        [HttpPostContract]
        void LogOff();
    }
}