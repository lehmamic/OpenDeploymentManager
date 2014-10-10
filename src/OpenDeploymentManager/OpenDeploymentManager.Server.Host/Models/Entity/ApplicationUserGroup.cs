using System;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ApplicationUserGroup
    {
        public Guid Id { get; set; }

        [UniqueConstraint]
        public string Name { get; set; }

        public PermissionMatrix GlobalPermissions { get; set; }
    }
}