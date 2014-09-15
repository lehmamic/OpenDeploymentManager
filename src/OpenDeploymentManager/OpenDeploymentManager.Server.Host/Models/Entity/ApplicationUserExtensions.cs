using System.Globalization;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Properties;

namespace OpenDeploymentManager.Server.Host.Models.Entity
{
    public static class ApplicationUserExtensions
    {
        public static string ToUserId(this string userName)
        {
            userName.ArgumentNotNull("userName");

            return string.Format(CultureInfo.InvariantCulture, Resources.ApplicationUserExtensions_ApplicationUserIdFormat, userName);
        }
    }
}