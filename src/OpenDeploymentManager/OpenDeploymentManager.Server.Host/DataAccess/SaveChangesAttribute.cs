using System;
using System.Net.Http;
using System.Web.Http.Filters;
using OpenDeploymentManager.Server.Host.Helpers;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SaveChangesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            var documentSession = actionExecutedContext.Request.GetDependencyScope().Resolve<IDocumentSession>();
            documentSession.SaveChanges();
        }
    }
}
