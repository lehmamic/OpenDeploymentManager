using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class HttpUrlParameterAttribute : HttpParameterBindingAttribute
    {
        #region Overrides of HttpParameterBindingAttribute
        public override void BindParameter(HttpParameterDescriptor parameter, HttpRequestMessage request)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var uriBuilder = new StringBuilder(request.RequestUri.ToString());

            string[] parameterAssignments = parameter.ParameterInfo.ParameterType.GetProperties()
                      .Select(p => new { Name = p.Name.ToLowerInvariant(), Value = p.GetValue(parameter.ParameterValue) })
                      .Where(p => p.Value != null)
                      .Select(p => string.Format(CultureInfo.InvariantCulture, "{0}={1}", p.Name, p.Value))
                      .ToArray();

            string postQueryString = string.IsNullOrEmpty(request.RequestUri.Query) ? "?" : "&";
            string queryString = string.Join("&", parameterAssignments).Insert(0, postQueryString);

            uriBuilder.Append(queryString);
            request.RequestUri = new Uri(uriBuilder.ToString(), UriKind.Relative);
        }
        #endregion
    }
}