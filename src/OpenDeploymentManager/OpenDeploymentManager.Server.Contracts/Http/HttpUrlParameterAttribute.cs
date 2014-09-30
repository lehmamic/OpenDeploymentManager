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

            var nameValuePairs = parameter.ParameterInfo.ParameterType.GetProperties()
                         .Select(p => new { Name = p.Name.ToLowerInvariant(), Value = p.GetValue(parameter.ParameterValue) })
                         .Where(p => p.Value != null);

            var query = request.GetUriQuery();
            foreach (var item in nameValuePairs)
            {
                query.Add(item.Name, item.Value);
            }
        }
        #endregion
    }
}