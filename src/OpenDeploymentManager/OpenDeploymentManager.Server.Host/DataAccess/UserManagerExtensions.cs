using System.Security.Claims;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class UserManagerExtensions
    {
        public static IdentityResult AddClaim<TUser>(this UserManager<TUser> userManager, string userId, string claimType, string claimValue) where TUser : class, IUser<string>
        {
            userManager.ArgumentNotNull("userManager");
            userId.ArgumentNotNullOrEmpty("userId");
            claimType.ArgumentNotNullOrEmpty("claimType");
            claimValue.ArgumentNotNullOrEmpty("claimValue");

            var claim = new Claim(claimType, claimValue);
            return userManager.AddClaim(userId, claim);
        }
    }
}