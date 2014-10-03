using Microsoft.AspNet.Identity;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ApplicationRole : IRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : this()
        {
            this.Name = roleName;
        }

        #region Implementation of IRole
        public string Id { get; set; }

        public string Name { get; set; }
        #endregion
    }
}
