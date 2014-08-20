using System;
using System.Linq;
using OpenDeploymentManager.Agent.Contracts;

namespace PiranhaDeploy.Agent.Contracts
{
    public static class ServiceRouteExtensions
    {
        public static string GetServiceRoute(this Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            string template = string.Empty;

            ServiceRouteAttribute attribute = serviceType.GetCustomAttributes(typeof(ServiceRouteAttribute), true)
                                                         .OfType<ServiceRouteAttribute>()
                                                         .FirstOrDefault();

            if (attribute != null)
            {
                template = attribute.Route;
            }

            return template;
        }
    }
}