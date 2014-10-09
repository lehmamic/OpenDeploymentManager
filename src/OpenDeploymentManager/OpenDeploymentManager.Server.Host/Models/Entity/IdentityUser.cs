using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class IdentityUser<TKey> : IUser<TKey>
        where TKey : struct
    {
        public IdentityUser()
        {
            this.Claims = new List<IdentityUserClaim>();
            this.Roles = new List<string>();
            this.Logins = new List<UserLoginInfo>();
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        #region Implementation of IUser<TKey>
        public virtual TKey Id { get; set; }

        [UniqueConstraint]
        public virtual string UserName { get; set; }
        #endregion

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual List<string> Roles { get; private set; }

        public virtual List<IdentityUserClaim> Claims { get; private set; }

        public virtual List<UserLoginInfo> Logins { get; private set; }
    }
}
