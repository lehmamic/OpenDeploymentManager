using System;
using System.Linq;
using System.Security.Claims;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Security;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.StartupTasks
{
    public class InitializeDatabaseTask : IStartupTask
    {
        private readonly IUnityContainer container;

        public InitializeDatabaseTask(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
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

        private static void CreateRoles(RoleManager<ApplicationRole> roleManager)
        {
            roleManager.CreateIfNotExist(RoleNames.Administrator);
        }

        private static void CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
            bool usersAvailable = userManager.Users.Any();
            if (!usersAvailable)
            {
                var userName = "Admin";
                var password = "123456";

                var adminUser = new ApplicationUser { UserName = userName };
                IdentityResult result = userManager.Create(adminUser, password);
                if (result.Succeeded)
                {
                    var adminRoleClaim = new Claim(ClaimTypes.Role, RoleNames.Administrator);
                    userManager.AddClaim(adminUser.Id, adminRoleClaim);
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
                CreateRoles(childContainer.Resolve<RoleManager<ApplicationRole>>());
                CreateAdminUser(childContainer.Resolve<UserManager<ApplicationUser>>());

                childContainer.Resolve<IDocumentSession>().SaveChanges();
            }
        }
    }
}
