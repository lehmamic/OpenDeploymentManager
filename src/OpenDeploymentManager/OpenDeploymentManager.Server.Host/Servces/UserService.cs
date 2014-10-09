using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager userManager;

        public UserService(ApplicationUserManager userManager)
        {
            this.userManager = userManager.ArgumentNotNull("userManager");
        }

        #region Implementation of IUserService
        public IQueryable<ApplicationUser> Query()
        {
            return this.userManager.Users;
        }

        public ApplicationUser GetById(Guid id)
        {
            return this.userManager.FindById(id);
        }

        public ApplicationUser GetByName(string userName)
        {
            return this.userManager.FindByName(userName);
        }

        public ApplicationUser GetApiKey(string apiKey)
        {
            throw new System.NotImplementedException();
        }

        public IdentityResult Create(ApplicationUser user, string password)
        {
            return this.userManager.Create(user, password);
        }

        public IdentityResult Update(ApplicationUser user)
        {
            return this.userManager.Update(user);
        }

        public IdentityResult Delete(ApplicationUser user)
        {
            return this.userManager.Delete(user);
        }

        public IdentityResult ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            return this.userManager.ChangePassword(userId, currentPassword, newPassword);
        }

        public IdentityResult SetPassword(Guid userId, string password)
        {
            IdentityResult result = this.userManager.RemovePassword(userId);
            if (result.Succeeded)
            {
                result = this.userManager.AddPassword(userId, password);
            }

            return result;
        }
        #endregion
    }
}