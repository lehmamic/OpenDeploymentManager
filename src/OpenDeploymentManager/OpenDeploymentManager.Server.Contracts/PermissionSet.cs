using System.Collections.Generic;

namespace OpenDeploymentManager.Server.Contracts
{
    public class PermissionSet
    {
        public PermissionSet()
        {
            this.ResourceTypes = new Dictionary<string, Permissions>();
        }

        public Dictionary<string, Permissions> ResourceTypes { get; set; }
    }
}