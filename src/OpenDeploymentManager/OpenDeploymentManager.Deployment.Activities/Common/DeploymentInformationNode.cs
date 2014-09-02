using System;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public class DeploymentInformationNode : IDeploymentInformationNode
    {
        private readonly Guid id;
        private readonly DateTimeOffset timestamp;
        private readonly IDeploymentInformationNode parent;
        private readonly IDeploymentInformationNodeCollection children;
        private readonly string content;

        public DeploymentInformationNode(Guid id, DateTimeOffset timestamp, IDeploymentInformationNode parent, string content)
        {
            this.id = id;
            this.timestamp = timestamp;
            this.parent = parent;
            this.children = new DeploymentInformationNodeCollection(parent);
            this.content = content;
        }

        #region Implementation of IDeploymentInformationNode
        public Guid Id
        {
            get
            {
                return this.id;
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
        }

        public DateTimeOffset Timestamp
        {
            get
            {
                return this.timestamp;
            }
        }

        public IDeploymentInformationNode Parent
        {
            get
            {
                return this.parent;
            }
        }

        public IDeploymentInformationNodeCollection Children
        {
            get
            {
                return this.children;
            }
        }
        #endregion
    }
}