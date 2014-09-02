using System.Activities;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public interface IDeploymentLoggingExtension
    {
        IActivityTracking GetActivityTracking(ActivityContext context);
    }
}