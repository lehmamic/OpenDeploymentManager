using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client
{
    public static class WebApiProxyExtensions
    {
        public static Uri GetRoute(this MethodInfo serviceOperations)
        {
            if (serviceOperations == null)
            {
                throw new ArgumentNullException("serviceOperations");
            }

            string template = string.Empty;

            OperationContractAttribute attribute = serviceOperations.GetCustomAttributes(typeof(OperationContractAttribute), true)
                                                  .OfType<OperationContractAttribute>()
                                                  .FirstOrDefault();

            if (attribute == null)
            {
                throw new ArgumentException("The service operation method has no OperationContract attribute", "serviceOperations");
            }

            template = attribute.Route;

            return new Uri(template, UriKind.Relative);
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