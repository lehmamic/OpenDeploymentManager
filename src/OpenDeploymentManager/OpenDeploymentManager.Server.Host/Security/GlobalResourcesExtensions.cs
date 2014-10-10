using System;
using OpenDeploymentManager.Server.Host.Properties;

namespace OpenDeploymentManager.Server.Host.Security
{
    public static class GlobalResourcesExtensions
    {
        public static string ToResourceName(this GlobalResources resource)
        {
            return resource.ToString().ToLowerInvariant();
        }

        public static GlobalResources FromResourceName(this string value)
        {
            GlobalResources resource;
            if (!Enum.TryParse(value, true, out resource))
            {
                throw new FormatException(Resources.GlobalResources_UnknownResourceName);
            }

            return resource;
        }

        public static bool IsValidResource(this string value)
        {
            GlobalResources resource;
            return Enum.TryParse(value, true, out resource);
        }

        public static string ToOperationName(this ResourceOperations operation)
        {
            return operation.ToString().ToLowerInvariant();
        }

        public static ResourceOperations FromOperationName(this string value)
        {
            ResourceOperations resource;
            if (!Enum.TryParse(value, true, out resource))
            {
                throw new FormatException(Resources.ResourceOperations_UnknownOperationName);
            }

            return resource;
        }
    }
}
