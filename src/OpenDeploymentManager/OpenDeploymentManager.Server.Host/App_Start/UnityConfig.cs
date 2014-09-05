using System.Web.Http;
using Microsoft.Practices.Unity;
using OpenDeploymentManager.Common.Diagnostics;
using Unity.WebApi;

namespace OpenDeploymentManager.Server.Host
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config, IUnityContainer container)
        {
            config.ArgumentNotNull("config");
            container.ArgumentNotNull("container");

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}