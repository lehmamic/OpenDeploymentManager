using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public class DeploymentInformationNodeCollection : IDeploymentInformationNodeCollection
    {
        private readonly IDeploymentInformationNode parent;
        private readonly List<IDeploymentInformationNode> nodes = new List<IDeploymentInformationNode>();

        public DeploymentInformationNodeCollection(IDeploymentInformationNode parent)
        {
            this.parent = parent;
        }

        #region Implementation of IDeploymentInformationNodeCollection
        public IDeploymentInformationNode CreateNode(string type, string content)
        {
            var node = new DeploymentInformationNode(Guid.NewGuid(), DateTimeOffset.Now, this.parent, content);
            this.nodes.Add(node);

            return node;
        }
        #endregion


        #region Implementation of IEnumerable
        public IEnumerator<IDeploymentInformationNode> GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}