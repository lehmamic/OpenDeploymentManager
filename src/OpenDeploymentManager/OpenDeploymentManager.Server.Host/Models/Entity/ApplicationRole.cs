using System;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }
    }
}
