using System.Linq;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Properties;
using Raven.Client.Linq;

namespace OpenDeploymentManager.Server.Host.Servces
{
    [SaveChanges]
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager.ArgumentNotNull("userManager");
        }

        #region Implementation of IUserService
        public IQueryable<ApplicationUser> Query()
        {
            return this.userManager.Users;
        }

        public ApplicationUser GetById(string id)
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
            ApplicationUser existingUser = this.userManager.FindById(user.UserName.ToUserId());
            if (existingUser != null)
            {
                return new IdentityResult(Resources.UserService_UserAlreadyExists);
            }

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

        public IdentityResult ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return this.userManager.ChangePassword(userId, currentPassword, newPassword);
        }

        public IdentityResult SetPassword(ApplicationUser user, string password)
        {
            user.ArgumentNotNull("user");

            IdentityResult result = this.userManager.RemovePassword(user.UserName);
            if (result.Succeeded)
            {
                result = this.userManager.AddPassword(user.UserName, password);
            }

            return result;
        }
        #endregion
    }
}