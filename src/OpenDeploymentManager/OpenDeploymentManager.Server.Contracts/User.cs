using System;
using System.ComponentModel.DataAnnotations;
using OpenDeploymentManager.Server.Contracts.Properties;

namespace OpenDeploymentManager.Server.Contracts
{
    public class User
    {
        public Guid Id { get; set; }

        [Display(Name = "Property_UserName", ResourceType = typeof(Resources))]
        public virtual string UserName { get; set; }

        [Display(Name = "Property_DisplayName", ResourceType = typeof(Resources))]
        public string DisplayName { get; set; }

        [Display(Name = "Property_Email", ResourceType = typeof(Resources))]
        [EmailAddress]
        public string Email { get; set; }
    }
}