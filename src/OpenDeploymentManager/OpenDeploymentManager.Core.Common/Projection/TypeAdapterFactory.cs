using System;

namespace OpenDeploymentManager.Core.Common.Projection
{
    public static class TypeAdapterFactory
    {
        #region Members

        private static ITypeAdapterFactory currentTypeAdapterFactory = null;

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Set the current type adapter factory
        /// </summary>
        /// <param name="adapterFactory">The adapter factory to set</param>
        public static void SetCurrent(ITypeAdapterFactory adapterFactory)
        {
            if (adapterFactory == null)
            {
                throw new ArgumentNullException("adapterFactory");
            }

            currentTypeAdapterFactory = adapterFactory;
        }

        /// <summary>
        /// Create a new type adapter from currect factory
        /// </summary>
        /// <returns>Created type adapter</returns>
        public static ITypeAdapter CreateAdapter()
        {
            if (currentTypeAdapterFactory == null)
            {
                throw new InvalidOperationException("No current type adapter factory available.");
            }

            return currentTypeAdapterFactory.Create();
        }
        #endregion
    }
}