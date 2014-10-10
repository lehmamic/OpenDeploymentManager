using OpenDeploymentManager.Server.Host.Security;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ResourcePermissionEntry
    {
        public string Resource { get; set; }

        public ResourceOperations PermittedOperations { get; set; }

        public void AddPermissions(ResourceOperations operations)
        {
            this.PermittedOperations = this.PermittedOperations | operations;
        }

        public void RemovePermissions(ResourceOperations operations)
        {
            this.PermittedOperations = this.PermittedOperations ^ operations;
        }

        public bool PermissionGranted(ResourceOperations operation)
        {
            return this.PermittedOperations.HasFlag(operation);
        }
    }
}
