namespace OpenDeploymentManager.Client
{
    public interface IOpenDeploymentManagerClient
    {
        TService GetService<TService>() where TService : class;
    }
}