namespace OpenDeploymentManager.Client
{
    public interface IOpenDeploymentManagerAuthentication
    {
        AuthenticationHeaderValue Authenticate(IUriResolver uriResolver);
    }
}