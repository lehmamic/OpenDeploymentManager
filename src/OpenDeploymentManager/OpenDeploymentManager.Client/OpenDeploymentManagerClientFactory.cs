namespace OpenDeploymentManager.Client
{
    public class OpenDeploymentManagerClientFactory : IOpenDeploymentManagerClientFactory
    {
        public IOpenDeploymentManagerClient CreateClient(OpenDeploymentManagerEndpoint serverEndpoint)
        {
            return new OpenDeploymentManagerClient(serverEndpoint);
        }
    }
}