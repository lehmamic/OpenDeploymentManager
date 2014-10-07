using System;
using System.Collections.Generic;
using OpenDeploymentManager.Server.Host.Security;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ApplicationUserGroup
    {
        public Guid Id { get; set; }

        [UniqueConstraint]
        public string Name { get; set; }

        public Dictionary<GlobalResourcePermission, ResourceOperations> GlobalPermissions { get; set; }
    }
}