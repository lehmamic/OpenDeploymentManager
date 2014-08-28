using System.Collections.Generic;

namespace OpenDeploymentManager.Deployment.Activities
{
    public interface IDeploymentInformationNodeCollection : IEnumerable<IDeploymentInformationNode>
    {
        IDeploymentInformationNode CreateNode(string type, string content);
    }
}