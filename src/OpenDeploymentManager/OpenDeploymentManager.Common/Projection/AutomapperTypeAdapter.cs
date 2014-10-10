using AutoMapper;

namespace OpenDeploymentManager.Common.Projection
{
    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutoMapperTypeAdapter : ITypeAdapter
    {
        #region ITypeAdapter Members

        public TTarget Adapt<TSource, TTarget>(TSource source) where TSource : class where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target) where TSource : class where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source, target);
        }

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return Mapper.Map<TTarget>(source);
        }

        #endregion
    }
}