using OpenDeploymentManager.Server.Host.Security;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public class ResourceOperationSet
    {
        public ResourceOperations PermittedOperations { get; set; }

        public void AddPermissions(ResourceOperations operations)
        {
            this.PermittedOperations = this.PermittedOperations | operations;
        }

        public void RemovePermissions(ResourceOperations operations)
        {
            this.PermittedOperations = this.PermittedOperations ^ operations;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ResourceOperationSet)obj);
        }

        public override int GetHashCode()
        {
            return (int)this.PermittedOperations;
        }

        protected bool Equals(ResourceOperationSet other)
        {
            return this.PermittedOperations == other.PermittedOperations;
        }
    }
}
