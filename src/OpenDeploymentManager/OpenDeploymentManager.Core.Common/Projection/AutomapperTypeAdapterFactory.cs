using AutoMapper;

namespace OpenDeploymentManager.Core.Common.Projection
{
    public class AutoMapperTypeAdapterFactory
        : ITypeAdapterFactory
    {
        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutoMapperTypeAdapter();
        }

        #endregion
    }
}