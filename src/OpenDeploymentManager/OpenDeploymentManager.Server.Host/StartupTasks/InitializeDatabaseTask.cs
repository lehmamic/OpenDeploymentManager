using System;
using System.Linq;
using System.Security.Claims;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
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

        private static void CreateAdminUser(IDocumentSession session)
        {
            bool usersAvailable = session.Query<ApplicationUser>().Any();
            if (!usersAvailable)
            {
                var userManager = new UserManager<ApplicationUser>(new ApplicationUserStore(session));

                var userName = "Admin";
                var password = "123456";
                var administratorRole = "Administrator";

                var adminUser = new ApplicationUser { UserName = userName };
                IdentityResult result = userManager.Create(adminUser, password);
                if (result.Succeeded)
                {
                    var adminRoleClaim = new Claim(ClaimTypes.Role, administratorRole);
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
            using (IDocumentSession session = this.container.Resolve<IDocumentStore>().OpenSession())
            {
                CreateAdminUser(session);
                session.SaveChanges();
            }
        }
    }
}
