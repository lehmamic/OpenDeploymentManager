using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class RoleManagerExtensions
    {
        public static void CreateIfNotExist<TRole>(this RoleManager<TRole> roleManager, string roleName) where TRole : class, IRole<string>, new()
        {
            roleManager.ArgumentNotNull("roleManager");
            roleName.ArgumentNotNullOrEmpty("roleName");

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new TRole { Name = roleName });
            }
        }
    }
}
