using System;
using System.ComponentModel.DataAnnotations;
using OpenDeploymentManager.Server.Contracts.Properties;

namespace OpenDeploymentManager.Server.Contracts
{
    public abstract class ResourceBase
    {
        #region Implementation of IResource
        [Display(Name = "Property_Created", ResourceType = typeof(Resources))]
        public Guid Id { get; set; }

        [Display(Name = "Property_Created", ResourceType = typeof(Resources))]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "Property_LastModified", ResourceType = typeof(Resources))]
        public DateTimeOffset LastModified { get; set; }
        #endregion
    }
}