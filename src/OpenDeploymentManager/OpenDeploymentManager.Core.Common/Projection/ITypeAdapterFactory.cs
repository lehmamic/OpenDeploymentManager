namespace OpenDeploymentManager.Core.Common.Projection
{
    /// <summary>
    /// Interface for adapter factory
    /// </summary>
    public interface ITypeAdapterFactory
    {
        /// <summary>
        /// Create a type adater
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();
    }
}