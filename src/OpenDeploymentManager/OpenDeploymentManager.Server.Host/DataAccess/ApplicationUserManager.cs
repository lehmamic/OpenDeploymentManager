using System;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
            : base(store)
        {
        }
    }
}
