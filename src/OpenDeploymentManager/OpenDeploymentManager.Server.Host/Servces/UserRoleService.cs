using System.Linq;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public class UserRoleService : IUserRoleService
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public UserRoleService(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager.ArgumentNotNull("roleManager");
        }

        #region Implementation of IUserRoleService
        public IQueryable<ApplicationRole> Query()
        {
            return this.roleManager.Roles;
        }

        public ApplicationRole GetById(string id)
        {
            return this.roleManager.FindById(id.ToRoleId());
        }

        public ApplicationRole GetByName(string roleName)
        {
            return this.roleManager.FindByName(roleName);
        }
        #endregion
    }
}