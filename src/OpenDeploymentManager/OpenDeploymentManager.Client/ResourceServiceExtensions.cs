using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client
{
    public static class WebApiProxyExtensions
    {
        public static string GetRoutePrefix(this Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            string template = string.Empty;

            HttpRequestUriPrefixAttribute attribute = serviceType.GetCustomAttributes(typeof(HttpRequestUriPrefixAttribute), true)
                                                  .OfType<HttpRequestUriPrefixAttribute>()
                                                  .FirstOrDefault();

            if (attribute != null)
            {
                template = attribute.RoutePrefix;
            }

            return template;
        }

        public static string GetRoute(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("serviceOperations");
            }

            string template = string.Empty;

            HttpRequestUriAttribute attribute = method.GetCustomAttributes(typeof(HttpRequestUriAttribute), true)
                                                  .OfType<HttpRequestUriAttribute>()
                                                  .FirstOrDefault();

            if (attribute != null)
            {
                template = attribute.Route;
            }

            return template;
        }

        public static HttpMethod GetHttpMethod(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            HttpMethod method = null;

            var attribute = methodInfo.GetCustomAttribute<HttpMethodContractAttribute>();
            if (attribute != null)
            {
                method = new HttpMethod(attribute.Method);
            }
            else if (methodInfo.Name.StartsWith("Get") || methodInfo.Name.StartsWith("Find") || methodInfo.Name.StartsWith("Query"))
            {
                method = HttpMethod.Get;
            }
            else if (methodInfo.Name.StartsWith("Post") || methodInfo.Name.StartsWith("Create"))
            {
                method = HttpMethod.Post;
            }
            else if (methodInfo.Name.StartsWith("Put") || methodInfo.Name.StartsWith("Update"))
            {
                method = HttpMethod.Put;
            }
            else if (methodInfo.Name.StartsWith("Delete"))
            {
                method = HttpMethod.Delete;
            }
            else
            {
                throw new ArgumentException("The http method could not been found on the method info.", "methodInfo");
            }

            return method;
        }
    }
}