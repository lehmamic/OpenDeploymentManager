using System.ComponentModel;

namespace OpenDeploymentManager.Server.Host.Security
{
    public enum GlobalResourcePermission
    {
        [Description("auth.ResourceAccessRule")]
        ResourceAccessRuleResource,

        [Description("auth.UserGroup")]
        UserGroupResource,

        [Description("auth.User")]
        UserResource
    }
}