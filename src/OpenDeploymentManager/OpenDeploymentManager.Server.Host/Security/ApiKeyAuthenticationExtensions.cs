////using Microsoft.Owin.Extensions;
////using OpenDeploymentManager.Common.Diagnostics;
////using Owin;

////namespace OpenDeploymentManager.Server.Host.Security
////{
////    public static class ApiKeyAuthenticationExtensions
////    {
////        public static IAppBuilder UseApiKeyAuthentication(this IAppBuilder app, ApiKeyAuthenticationOptions options)
////        {
////            app.ArgumentNotNull("app");
////            options.ArgumentNotNull("options");

////            app.Use(typeof(ApiKeyAuthenticationMiddleware), app, options);
////            app.UseStageMarker(PipelineStage.Authenticate);

////            return app;
////        }
////    }
////}