using System;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class HttpRouteParameterBindingAttribute : HttpParameterBindingAttribute
    {
        #region Overrides of HttpParameterBindingAttribute
        public override void BindParameter(HttpParameterDescriptor parameter, HttpRequestMessage request)
        {
            var uriBuilder = new StringBuilder(request.RequestUri.ToString().ToLowerInvariant());

            string variable = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", parameter.ParameterName).ToLowerInvariant();
            uriBuilder.Replace(variable, parameter.ParameterValue.ToString());

            request.RequestUri = new Uri(uriBuilder.ToString(), UriKind.Relative);
        }
        #endregion
    }
}