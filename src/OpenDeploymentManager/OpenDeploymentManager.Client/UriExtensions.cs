using System;

namespace OpenDeploymentManager.Client
{
    internal static class UriExtensions
    {
        public static Uri ExtractBaseUri(this Uri uri, string route)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            if (route == null)
            {
                throw new ArgumentNullException("route");
            }

            string uriString = uri.ToString().AppendIfNotEndsWith("/");
            int length = uriString.LastIndexOf(route.AppendIfNotEndsWith("/"), StringComparison.OrdinalIgnoreCase);
            if (length >= 1)
            {
                uriString = uriString.Substring(0, length);
            }

            return new Uri(uriString.TrimEnd('/'));
        }

        public static Uri ExtractRootUri(this Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            return new Uri(uri.Scheme + "://" + uri.Authority);
        }
    }
}