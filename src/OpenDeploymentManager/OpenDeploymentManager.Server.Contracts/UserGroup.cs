using System;
using System.ComponentModel.DataAnnotations;
using OpenDeploymentManager.Server.Contracts.Properties;

namespace OpenDeploymentManager.Server.Contracts
{
    public class UserGroup
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Property_UserGroupName", ResourceType = typeof(Resources))]
        public string Name { get; set; }
    }
}
