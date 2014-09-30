using System;
using System.Globalization;

namespace OpenDeploymentManager.Client
{
    internal static class StringExtensions
    {
        public static string AppendIfNotEndsWith(this string source, string value)
        {
            return source.EndsWith(value)
                       ? source
                       : string.Format(CultureInfo.InvariantCulture, "{0}{1}", source, value);
        }

        public static Uri ToUri(this string url)
        {
            return ToUri(url, UriKind.RelativeOrAbsolute);
        }

        public static Uri ToUri(this string url, UriKind uriKind)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            return new Uri(url, uriKind);
        }
    }
}