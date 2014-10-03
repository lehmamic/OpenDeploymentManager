using System.Globalization;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Properties;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public static class ApplicationRoleExtensions
    {
        public static string ToRoleId(this string roleName)
        {
            roleName.ArgumentNotNull("roleName");

            return string.Format(CultureInfo.InvariantCulture, Resources.ApplicationRoleExtensions_ApplicationRoleIdFormat, roleName);
        }
    }
}