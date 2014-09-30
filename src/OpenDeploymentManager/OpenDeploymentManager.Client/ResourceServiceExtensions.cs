using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

            HttpRequestContractAttribute attribute = serviceOperations.GetCustomAttributes(typeof(HttpRequestContractAttribute), true)
                                                  .OfType<HttpRequestContractAttribute>()
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

        public static Uri GetRequestUri(this MethodInfo method, object[] arguments)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            var uriBuilder = new StringBuilder(method.GetRoute().ToString().ToLowerInvariant());

            ParameterInfo[] parameters = method.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                object parameterValue = arguments[i];

                if (parameterValue == null)
                {
                    throw new ArgumentNullException(parameter.Name);
                }

                string variable = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", parameter.Name).ToLowerInvariant();
                uriBuilder.Replace(variable, parameterValue.ToString());

                if (parameter.IsDefined(typeof(HttpUrlParameterAttribute), true))
                {
                    string[] parameterAssignments = parameter.ParameterType.GetProperties()
                              .Where(p => p.GetValue(parameterValue) != null || p.GetValue(parameterValue) != (object)0)
                              .Select(p => string.Format(CultureInfo.InvariantCulture, "${0}={1}", p.Name.ToLowerInvariant(), p.GetValue(parameterValue)))
                              .ToArray();

                    string queryString = string.Join("&", parameterAssignments).Insert(0, "?");

                    uriBuilder.Append(queryString);
                }
            }

            return new Uri(uriBuilder.ToString(), UriKind.Relative);
        }
    }
}