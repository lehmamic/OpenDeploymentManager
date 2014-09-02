namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public interface IDeploymentVariablesExtension
    {
        T Get<T>(string key);
    }
}