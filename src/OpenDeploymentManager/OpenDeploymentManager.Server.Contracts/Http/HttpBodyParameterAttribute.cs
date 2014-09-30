using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class HttpBodyParameterAttribute : HttpParameterBindingAttribute
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

            request.Content = new ObjectContent(parameter.ParameterInfo.ParameterType, parameter.ParameterValue, new JsonMediaTypeFormatter());
        }
        #endregion
    }
}
