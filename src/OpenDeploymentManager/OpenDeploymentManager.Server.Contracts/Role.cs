using System.ComponentModel.DataAnnotations;
using OpenDeploymentManager.Server.Contracts.Properties;

namespace OpenDeploymentManager.Server.Contracts
{
    public class Role
    {
        [Display(Name = "Property_RoleName", ResourceType = typeof(Resources))]
        public string Name { get; set; }
    }
}
