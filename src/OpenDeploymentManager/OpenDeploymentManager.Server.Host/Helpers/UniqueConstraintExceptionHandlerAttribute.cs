using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using AutoMapper;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Properties;

namespace OpenDeploymentManager.Server.Host.Helpers
{
    public class UniqueConstraintExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception as UniqueConstraintException;
            if (exception != null)
            {
                var modelState = new ModelStateDictionary();
                TypeMap map = FindTypeMap(actionExecutedContext, exception);
                if (map != null)
                {
                    IEnumerable<PropertyMap> mappedProperties = exception.Properties.Join(
                        map.GetPropertyMaps(),
                        outer => outer.Name,
                        inner => inner.SourceMember.Name,
                        (outer, inner) => inner);

                    foreach (PropertyMap property in mappedProperties)
                    {
                        modelState.AddModelError(
                            property.DestinationProperty.Name,
                            string.Format(CultureInfo.CurrentUICulture, Resources.UniqueConstraintExceptionHandlerAttribute_MustBeUnique, property.DestinationProperty.Name));
                    }
                }
                else
                {
                    modelState.AddModelError(string.Empty, exception.Message);
                }

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, modelState);
            }
        }

        private static TypeMap FindTypeMap(HttpActionExecutedContext actionExecutedContext, UniqueConstraintException exception)
        {
            TypeMap map = null;
            if (exception.Properties.Any())
            {
                map = actionExecutedContext.Request.GetActionDescriptor()
                                         .GetParameters()
                                         .Select(p => p.ParameterType)
                                         .Select(p => Mapper.FindTypeMapFor(exception.Properties[0].DeclaringType, p))
                                         .FirstOrDefault(m => m != null);
            }

            return map;
        }
    }
}
