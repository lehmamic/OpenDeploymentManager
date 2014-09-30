using System;
using System.Globalization;
using System.Linq;
using OpenDeploymentManager.Client.Http.Pipeline;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class DispatchHttpRequestParameterMiddleware : IMiddleware
    {
        #region Implementation of IMiddleware
        public IHttpResponseContext Invoke(IHttpRequestContext context, INextInvoker next)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            foreach (HttpParameterDescriptor parameter in context.Parameters)
            {
                string uriVariableTemplate = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", parameter.ParameterName).ToLowerInvariant();

                HttpParameterBindingAttribute binding = null;
                if (parameter.ParameterInfo.IsDefined(typeof(HttpParameterBindingAttribute), true))
                {
                    binding = parameter.ParameterInfo.GetCustomAttributes(typeof(HttpParameterBindingAttribute), true)
                        .OfType<HttpParameterBindingAttribute>()
                        .First();
                }
                else if (parameter.ParameterInfo.ParameterType.IsDefined(typeof(HttpParameterBindingAttribute), true))
                {
                    binding = parameter.ParameterInfo.ParameterType.GetCustomAttributes(typeof(HttpParameterBindingAttribute), true)
                        .OfType<HttpParameterBindingAttribute>()
                        .First();
                }
                else if (context.Request.RequestUri.ToString().ToLowerInvariant().Contains(uriVariableTemplate))
                {
                    binding = new HttpRouteParameterBindingAttribute();
                }
                else if (parameter.ParameterInfo.ParameterType.IsPrimitive || parameter.ParameterInfo.ParameterType == typeof(string))
                {
                    binding = new HttpUrlParameterAttribute();
                }
                else
                {
                    binding = new HttpBodyParameterAttribute();
                }

                binding.BindParameter(parameter, context.Request);
            }

            return next.Invoke(context);
        }
        #endregion
    }
}
