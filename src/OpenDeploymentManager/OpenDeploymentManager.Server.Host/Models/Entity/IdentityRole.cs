using Microsoft.AspNet.Identity;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class IdentityRole<TKey> : IRole<TKey>
        where TKey : struct
    {
        public IdentityRole()
        {
        }

        public IdentityRole(string roleName)
            : this()
        {
            this.Name = roleName;
        }

        #region Implementation of IRole
        public TKey Id { get; set; }

        [UniqueConstraint]
        public string Name { get; set; }
        #endregion
    }
}