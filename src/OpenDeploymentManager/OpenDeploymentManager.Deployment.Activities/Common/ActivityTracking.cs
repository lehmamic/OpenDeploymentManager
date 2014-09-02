namespace OpenDeploymentManager.Deployment.Activities.Common
{
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
}