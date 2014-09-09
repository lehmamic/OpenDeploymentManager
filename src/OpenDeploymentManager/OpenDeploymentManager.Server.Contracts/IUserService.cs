using System;

namespace OpenDeploymentManager.Server.Contracts
{
    [Route("Users")]
    public interface IUserService : IResourceService<User>
    {
        void SetPassword(Guid id, string password, string confirmPassword);
    }
}
