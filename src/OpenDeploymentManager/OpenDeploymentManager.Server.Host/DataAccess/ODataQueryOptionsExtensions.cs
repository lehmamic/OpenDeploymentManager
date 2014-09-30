using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using AutoMapper;
using Microsoft.Data.Edm;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class ODataQueryOptionsExtensions
    {
        public static IQueryable<T> ApplyTo<T>(this ODataQueryOptions<T> queryOptions, IQueryable<T> query)
        {
            queryOptions.ArgumentNotNull("queryOptions");
            var querySettings = new ODataQuerySettings
            {
                EnableConstantParameterization = false,
                EnsureStableOrdering = true,
            };

            return (IQueryable<T>)queryOptions.ApplyTo(query, querySettings);
        }

        public static IEdmModel CreateEdmModel<TEntity>(this HttpRequestMessage request)
        {
            return request.CreateEdmModel(typeof(TEntity));
        }

        public static IEdmModel CreateEdmModel(this HttpRequestMessage request, Type entityType)
        {
            request.ArgumentNotNull("request");
            entityType.ArgumentNotNull("entityType");

            var modelBuilder = new ODataConventionModelBuilder(request.GetRequestContext().Configuration);
            modelBuilder.AddEntity(entityType);

            return modelBuilder.GetEdmModel();
        }

        public static ODataQueryContext CreateODataQueryContext<TEntity>(this HttpRequestMessage request)
        {
            request.ArgumentNotNull("request");

            return new ODataQueryContext(request.CreateEdmModel<TEntity>(), typeof(TEntity));
        }

        public static HttpRequestMessage Map<TDto, TEntity>(this HttpRequestMessage request)
        {
            request.ArgumentNotNull("request");

            // TODO: Map Property from Dto to Entity
            var propertyMap = Mapper.FindTypeMapFor<TDto, TEntity>().GetPropertyMaps()
                .Where(m => !m.IsIgnored())
                .Where(m => m.SourceMember.Name != "value")
                .ToDictionary(m => m.SourceMember.Name, m => m.DestinationProperty.Name, StringComparer.InvariantCultureIgnoreCase);



            return request;
        }
    }
}