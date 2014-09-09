using System;

namespace OpenDeploymentManager.Server.Contracts
{
    public interface IResource
    {
        Guid Id { get; set; }

        DateTimeOffset Created { get; }

        DateTimeOffset LastModified { get; }
    }
}