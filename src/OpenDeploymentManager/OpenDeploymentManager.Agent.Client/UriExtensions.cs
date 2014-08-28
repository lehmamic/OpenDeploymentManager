using System;
using PiranhaDeploy.Agent.Contracts;

namespace OpenDeploymentManager.Agent.Client
{
    public static class UriExtensions
    {
        public static Uri BaseAddress<TContract>(this Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            string uriString = uri.ToString();
            int indexOfRoute = uriString.IndexOf(typeof(TContract).GetServiceRoute(), StringComparison.Ordinal);

            return indexOfRoute > 0 ? new Uri(uriString.Remove(indexOfRoute)) : uri;
        }
    }
}
