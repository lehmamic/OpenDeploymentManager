using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenDeploymentManager.Server.Host.Security;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class PermissionMatrix
    {
        private readonly IDictionary<string, ResourceOperationSet> matrix = new Dictionary<string, ResourceOperationSet>();

        public void AddOperations(string resource, ResourceOperations operations)
        {
            if (!this.matrix.ContainsKey(resource))
            {
                this.matrix.Add(resource, new ResourceOperationSet());
            }

            var resourcePermission = this.matrix[resource];
            resourcePermission.AddPermissions(operations);
        }

        public void RemoveOperations(string resource, ResourceOperations operations)
        {
            if (!this.matrix.ContainsKey(resource))
            {
                this.matrix.Add(resource, new ResourceOperationSet());
            }

            var resourcePermission = this.matrix[resource];
            resourcePermission.RemovePermissions(operations);
        }

        public IReadOnlyDictionary<string, ResourceOperationSet> GetMatrix()
        {
            return new ReadOnlyDictionary<string, ResourceOperationSet>(this.matrix);
        }
    }
}
