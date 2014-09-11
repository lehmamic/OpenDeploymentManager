using System.Web.Http.Dependencies;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.Helpers
{
    public static class DependencyResolverExtensions
    {
        public static TService Resolve<TService>(this IDependencyScope dependencyScope)
        {
            dependencyScope.ArgumentNotNull("dependencyScope");

            return (TService)dependencyScope.GetService(typeof(TService));
        }
    }
}