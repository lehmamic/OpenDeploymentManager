using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class UserManagerExtensions
    {
        public static IdentityResult AddClaim<TUser, TKey>(this UserManager<TUser, TKey> userManager, TKey userId, string claimType, string claimValue) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
        {
            userManager.ArgumentNotNull("userManager");
            claimType.ArgumentNotNullOrEmpty("claimType");
            claimValue.ArgumentNotNullOrEmpty("claimValue");

            var claim = new Claim(claimType, claimValue);
            return userManager.AddClaim(userId, claim);
        }
    }
}