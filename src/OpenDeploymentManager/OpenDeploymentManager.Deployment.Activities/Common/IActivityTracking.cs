using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public interface IActivityTracking
    {
        IDeploymentInformationNode Node { get; }
    }

    public class ActivityTracking : IActivityTracking
    {
        private readonly IDeploymentInformationNode node;

        public ActivityTracking(IDeploymentInformationNode node)
        {
            this.node = node;
        }

        #region Implementation of IActivityTracking
        public IDeploymentInformationNode Node
        {
            get
            {
                return this.node;
            }
        }
        #endregion
    }

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