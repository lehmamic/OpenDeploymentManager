using System.ComponentModel;

namespace Jetpack.NET.API.Security
{
    public enum PermissionResource
    {
        [Description("auth.ResourceAccessRule")]
        ResourceAccessRuleResource,

        [Description("auth.Role")]
        RoleResource,

        [Description("auth.User")]
        UserResource
    }
}