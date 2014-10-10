using System;
using System.Collections.ObjectModel;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ApplicationUserGroup
    {
        public ApplicationUserGroup()
        {
            this.GlobalPermissions = new Collection<ResourcePermissionEntry>();
        }

        public Guid Id { get; set; }

        [UniqueConstraint]
        public string Name { get; set; }

        public Collection<ResourcePermissionEntry> GlobalPermissions { get; set; }
    }
}