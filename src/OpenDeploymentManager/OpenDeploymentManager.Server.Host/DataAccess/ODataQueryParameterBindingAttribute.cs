using System;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    /// <summary>
    /// A <see cref="T:System.Web.Http.ParameterBindingAttribute"/> to bind parameters of type <see cref="T:System.Web.Http.OData.Query.ODataQueryOptions"/> to the OData query from the incoming request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class ODataQueryParameterBindingAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            return (HttpParameterBinding)new ODataQueryParameterBindingAttribute.ODataQueryParameterBinding(parameter);
        }

        internal struct AsyncVoid
        {
        }

        internal class ODataQueryParameterBinding : HttpParameterBinding
        {
            private static readonly MethodInfo CreateODataQueryOptionsMethod = typeof(ODataQueryParameterBinding).GetMethod("CreateODataQueryOptions");

            public ODataQueryParameterBinding(HttpParameterDescriptor parameterDescriptor)
                : base(parameterDescriptor)
            {
            }

            public static ODataQueryOptions<TDto, TEntity> CreateODataQueryOptions<TDto, TEntity>(ODataQueryContext context, HttpRequestMessage request)
            {
                return new ODataQueryOptions<TDto, TEntity>(context, request);
            }

            public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
            {
                actionContext.ArgumentNotNull("actionContext");

                HttpRequestMessage request = actionContext.Request;
                if (request == null)
                {
                    throw new ArgumentException("actionContext", "The action context must have a request.");
                }

                if (request.GetRequestContext().Configuration == null)
                {
                    throw new ArgumentException("actionContext", "The request must have a configuration.");
                }

                Type entityClrType = GetEntityClrTypeFromParameterType(this.Descriptor);
                Type dtoClrType = GetDtoClrTypeFromParameterType(this.Descriptor);

                var odataQueryContext = new ODataQueryContext(request.CreateEdmModel(entityClrType), entityClrType);

                var odataQueryOptions = (ODataQueryOptions)CreateODataQueryOptionsMethod.MakeGenericMethod(dtoClrType, entityClrType)
                                             .Invoke(null, new object[] { odataQueryContext, request });


                this.SetValue(actionContext, (object)odataQueryOptions);
                return Task.FromResult(new AsyncVoid());
            }

            internal static Type GetEntityClrTypeFromParameterType(HttpParameterDescriptor parameterDescriptor)
            {
                Type parameterType = parameterDescriptor.ParameterType;

                if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(ODataQueryOptions<,>))
                {
                    return parameterType.GetGenericArguments()[1];
                }
                else
                {
                    throw new ArgumentException("The action parameter type must be ODataQueryOptions<TDto, TEntity>.", "parameterDescriptor");
                }
            }

            internal static Type GetDtoClrTypeFromParameterType(HttpParameterDescriptor parameterDescriptor)
            {
                Type parameterType = parameterDescriptor.ParameterType;

                if (parameterType.IsGenericType && parameterType.GetGenericTypeDefinition() == typeof(ODataQueryOptions<,>))
                {
                    return parameterType.GetGenericArguments()[0];
                }
                else
                {
                    throw new ArgumentException("The action parameter type must be ODataQueryOptions<TDto, TEntity>.", "parameterDescriptor");
                }
            }
        }
    }
}
