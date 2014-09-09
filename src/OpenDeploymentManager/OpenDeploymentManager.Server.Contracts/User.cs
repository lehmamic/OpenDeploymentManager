using System;

namespace OpenDeploymentManager.Server.Contracts
{
    public class User : IResource
    {
        #region Implementation of IResource
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; private set; }

        public DateTimeOffset LastModified { get; private set; }
        #endregion
    }
}