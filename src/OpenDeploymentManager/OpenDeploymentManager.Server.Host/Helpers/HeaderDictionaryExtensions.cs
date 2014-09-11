using Microsoft.Owin;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.Helpers
{
    public static class HeaderDictionaryExtensions
    {
        public static bool TryGetValue(this IHeaderDictionary source, string key, out string value)
        {
            source.ArgumentNotNull("source");
            key.ArgumentNotNull("key");

            value = source.Get(key);
            return !string.IsNullOrEmpty(value);
        }
    }
}