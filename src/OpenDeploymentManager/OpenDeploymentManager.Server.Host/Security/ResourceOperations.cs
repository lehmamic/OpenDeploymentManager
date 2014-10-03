using System;
using System.ComponentModel;

namespace OpenDeploymentManager.Server.Host.Security
{
    [Flags]
    public enum ResourceOperations
    {
        [Description("NONE")]
        None = 0,

        [Description("READ")]
        Read = 1,

        [Description("READALL")]
        ReadAll = 2,

        [Description("DELETE")]
        Delete = 4,

        [Description("CREATE")]
        Create = 8,

        [Description("UPDATE")]
        Update = 16,

        [Description("ALL")]
        All = (Read | ReadAll | Delete | Create | Update)
    }
}
