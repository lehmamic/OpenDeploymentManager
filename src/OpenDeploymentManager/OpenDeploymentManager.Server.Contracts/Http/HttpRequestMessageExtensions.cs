using System;
using System.Net.Http;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    public static class HttpRequestMessageExtensions
    {
        private const string UriQueryPropertyKey = "UriQueryProperty";

        public static UriQuery GetUriQuery(this HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (!request.Properties.ContainsKey(UriQueryPropertyKey))
            {
                request.Properties.Add(UriQueryPropertyKey, new UriQuery());
            }

            return (UriQuery)request.Properties[UriQueryPropertyKey];
        }
    }
}
