namespace OpenDeploymentManager.Deployment
{
    public interface IDeploymentManager
    {
        void RunDeployment(string template, object variables);
    }
}