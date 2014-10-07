using System.Web.Http;
using System.Web.Http.OData.Extensions;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Helpers;

namespace OpenDeploymentManager.Server.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.ArgumentNotNull("config");

            // Web API configuration and services
            config.AddODataQueryFilter();

            config.Filters.Add(new UniqueConstraintExceptionHandlerAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
