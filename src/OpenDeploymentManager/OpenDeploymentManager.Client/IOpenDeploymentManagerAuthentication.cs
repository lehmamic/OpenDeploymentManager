using OpenDeploymentManager.Client.Http;

namespace OpenDeploymentManager.Client
{
    public interface IOpenDeploymentManagerAuthentication
    {
        AuthenticationHeaderValue Authenticate(IUriResolver uriResolver);
    }
}