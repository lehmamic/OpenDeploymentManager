using System;
using System.Globalization;

namespace OpenDeploymentManager.Agent.Client
{
    public static class UriExtensions
    {
        public static Uri BaseAddress(this Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            return new Uri(string.Format(CultureInfo.InvariantCulture, "{0}://{1}", uri.Scheme, uri.Authority));
        }
    }
}
