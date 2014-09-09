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
    }
}