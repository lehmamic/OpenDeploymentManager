using System;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, Guid> store)
            : base(store)
        {
        }
    }
}