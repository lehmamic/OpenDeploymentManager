using System.Linq;
using System.Security.Claims;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Properties;
using OpenDeploymentManager.Server.Host.Security;
using OpenDeploymentManager.Server.Host.Servces;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.StartupTasks
{
    public class InitializeDatabaseTask : IStartupTask
    {
        private readonly IUnityContainer container;

        public InitializeDatabaseTask(IUnityContainer container)
        {
            this.container = container.ArgumentNotNull("container");
        }

        #region Implementation of IStartupTask
        public void Run()
        {
            this.CreateIndexes();
            this.Seed();
        }

        public void Reset()
        {
        }
        #endregion

        private static void CreateUserGroups(IUserGroupService userGroupService)
        {
            if (userGroupService.GetById(WellKnownEntityKeys.AdministratorsUserGroup) == null)
            {
                var administratorsGroup = new ApplicationUserGroup
                                              {
                                                  Id = WellKnownEntityKeys.AdministratorsUserGroup,
                                                  Name = Resources.UserGroup_Administrators
                                              };

                userGroupService.Create(administratorsGroup);
            }
        }

        private static void CreateRoles(ApplicationRoleManager roleManager)
        {
            roleManager.CreateIfNotExist(RoleNames.Administrator);
        }

        private static void CreateAdminUser(ApplicationUserManager userManager)
        {
            bool usersAvailable = userManager.Users.Any();
            if (!usersAvailable)
            {
                var userName = "Admin";
                var password = "123456";

                var adminUser = new ApplicationUser { Id = WellKnownEntityKeys.AdministratorUser, UserName = userName };
                IdentityResult result = userManager.Create(adminUser, password);
                if (result.Succeeded)
                {
                    userManager.AddClaim(adminUser.Id, ClaimTypes.Role, RoleNames.Administrator);
                    userManager.AddClaim(adminUser.Id, ClaimTypes.GroupSid, WellKnownEntityKeys.AdministratorsUserGroup.ToString());
                }
            }
        }

        private void CreateIndexes()
        {
            ///IndexCreation.CreateIndexes(typeof(EnvironmentMaxOrderIndex).Assembly, this.container.Resolve<IDocumentStore>());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The handling of the lifetime is under control.")]
        private void Seed()
        {
            using (IUnityContainer childContainer = this.container.CreateChildContainer())
            {
                CreateUserGroups(childContainer.Resolve<IUserGroupService>());
                CreateRoles(childContainer.Resolve<ApplicationRoleManager>());
                CreateAdminUser(childContainer.Resolve<ApplicationUserManager>());

                childContainer.Resolve<IDocumentSession>().SaveChanges();
            }
        }
    }
}
