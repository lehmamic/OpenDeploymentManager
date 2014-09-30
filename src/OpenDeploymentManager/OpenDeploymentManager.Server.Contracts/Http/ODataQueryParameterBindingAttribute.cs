using System;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class ODataQueryParameterBindingAttribute : HttpParameterBindingAttribute
    {
        #region Overrides of ParameterBindingAttribute
        public override void BindParameter(HttpParameterDescriptor parameter, HttpRequestMessage request)
        {
            var odataQuery = (PagingQuery)parameter.ParameterValue;
            var uriBuilder = new StringBuilder(request.RequestUri.ToString());

            if (odataQuery.Top != 0)
            {
                uriBuilder.Append(CreateKeyValuePair("top", odataQuery.Top));
            }

            if (odataQuery.Skip != 0)
            {
                uriBuilder.Append(CreateKeyValuePair("skip", odataQuery.Top));
            }

            request.RequestUri = new Uri(uriBuilder.ToString(), UriKind.Relative);
        }
        #endregion

        private static string CreateKeyValuePair(string key, object value)
        {
            return string.Format(CultureInfo.InvariantCulture, "${0}={1}", key, value);
        }
    }
}