namespace OpenDeploymentManager.Client
{
    public interface IOpenDeploymentManagerClientFactory
    {
        IOpenDeploymentManagerClient CreateClient(OpenDeploymentManagerEndpoint serverEndpoint);
    }
}