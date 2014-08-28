using System.Collections.Generic;

namespace OpenDeploymentManager.Common.Projection
{
    public static class ProjectionsExtensions
    {
        /// <summary>
        /// Project a type using a DTO
        /// </summary>
        /// <typeparam name="TProjection">The dto projection</typeparam>
        /// <param name="item">The source entity to project</param>
        /// <returns>The projected type</returns>
        public static TProjection ProjectedAs<TProjection>(this object item)
            where TProjection : class,new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TProjection>(item);
        }

        /// <summary>
        /// Project a type using a DTO
        /// </summary>
        /// <typeparam name="TProjection">The dto projection</typeparam>
        /// <typeparam name="TSource">The source type of the projection</typeparam>
        /// <param name="item">The source entity to project</param>
        /// <param name="instance"></param>
        /// <returns>The projected type</returns>
        public static TProjection ProjectedTo<TSource, TProjection>(this TSource item, TProjection instance)
            where TProjection : class where TSource : class
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<TSource, TProjection>(item, instance);
        }

        /// <summary>
        /// projected a enumerable collection of items
        /// </summary>
        /// <typeparam name="TProjection">The dtop projection type</typeparam>
        /// <param name="items">the collection of entity items</param>
        /// <returns>Projected collection</returns>
        public static IEnumerable<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<object> items)
            where TProjection : class, new()
        {
            var adapter = TypeAdapterFactory.CreateAdapter();
            return adapter.Adapt<IEnumerable<TProjection>>(items);
        }
    }
}