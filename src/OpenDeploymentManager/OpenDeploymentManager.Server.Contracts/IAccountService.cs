namespace OpenDeploymentManager.Server.Contracts
{
    [Route("Account")]
    public interface IAccountService
    {
        User GetMe();

        void ChangePassword(string password, string confirmPassword);
    }
}