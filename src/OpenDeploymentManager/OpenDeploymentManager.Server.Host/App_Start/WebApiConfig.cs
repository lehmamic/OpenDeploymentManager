using System.Web.Http;
using System.Web.Http.OData.Extensions;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.ArgumentNotNull("config");

            // Web API configuration and services
            config.AddODataQueryFilter();

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
