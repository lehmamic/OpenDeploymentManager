using System;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class RoleManagerExtensions
    {
        public static void CreateIfNotExist<TRole, TKey>(this RoleManager<TRole, TKey> roleManager, string roleName) where TRole : class, IRole<TKey>, new() where TKey : IEquatable<TKey>
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
