using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using OpenDeploymentManager.Server.Contracts;

namespace OpenDeploymentManager.Client
{
    public static class ResourceServiceExtensions
    {
        public static Uri GetRoute(this Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            string template = string.Empty;

            RouteAttribute attribute = serviceType.GetCustomAttributes(typeof(RouteAttribute), true)
                                                  .OfType<RouteAttribute>()
                                                  .FirstOrDefault();

            if (attribute != null)
            {
                template = attribute.Route;
            }
            else
            {
                template = serviceType.Name.TrimStart('I').Replace("Service", string.Empty);
            }

            return new Uri(template, UriKind.Relative);
        }

        public static HttpMethod GetHttpMethod(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            var method = HttpMethod.Get;

            var attribute = methodInfo.GetCustomAttribute<HttpMethodAttribute>();
            if (attribute != null)
            {
                method = (HttpMethod)Enum.Parse(typeof(HttpMethod), attribute.Method);
            }

            return method;
        }
    }
}