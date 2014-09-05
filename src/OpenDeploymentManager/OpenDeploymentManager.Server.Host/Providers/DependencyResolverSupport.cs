using System;
using System.Web.Http.Dependencies;

namespace PiranhaDeploy.Server.Api.Identity.Providers
{
    public static class DependencyResolverSupport
    {
        public static TService Resolve<TService>(this IDependencyScope dependencyScope)
        {
            if (dependencyScope == null)
            {
                throw new ArgumentNullException("dependencyScope");
            }

            return (TService)dependencyScope.GetService(typeof(TService));
        }
    }
}