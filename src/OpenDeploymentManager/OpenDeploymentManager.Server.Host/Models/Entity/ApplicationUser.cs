using System;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
