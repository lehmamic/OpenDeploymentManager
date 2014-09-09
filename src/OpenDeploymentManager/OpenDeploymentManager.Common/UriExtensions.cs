using System;

namespace OpenDeploymentManager.Common
{
    public static class UriExtensions
    {
        public static Uri ToUri(this string value)
        {
            return new Uri(value);
        }
    }
}