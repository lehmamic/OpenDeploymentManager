using System;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public interface IDeploymentInformationNode
    {
        string Content { get; }

        DateTimeOffset Timestamp { get; }

        IDeploymentInformationNode Parent { get; }

        IDeploymentInformationNodeCollection Children { get; }

        Guid Id { get; }
    }
}