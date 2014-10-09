using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public interface IUserService
    {
        IQueryable<ApplicationUser> Query();

        ApplicationUser GetById(Guid id);

        ApplicationUser GetByName(string userName);

        ApplicationUser GetApiKey(string apiKey);

        IdentityResult Create(ApplicationUser user, string password);

        IdentityResult Update(ApplicationUser user);

        IdentityResult Delete(ApplicationUser user);

        IdentityResult ChangePassword(Guid userId, string currentPassword, string newPassword);

        IdentityResult SetPassword(Guid userId, string password);
    }
}
