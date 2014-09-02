using System.Collections.Generic;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public interface IDeploymentInformationNodeCollection : IEnumerable<IDeploymentInformationNode>
    {
        IDeploymentInformationNode CreateNode(string type, string content);
    }
}