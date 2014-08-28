using System.Activities;

namespace OpenDeploymentManager.Deployment.Activities
{
    public interface IDeploymentLoggingExtension
    {
        IActivityTracking GetActivityTracking(ActivityContext context);
    }
}