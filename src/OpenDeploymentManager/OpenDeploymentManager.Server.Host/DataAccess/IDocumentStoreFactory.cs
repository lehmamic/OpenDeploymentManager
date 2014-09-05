using Raven.Client;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public interface IDocumentStoreFactory
    {
        IDocumentStore CreateDocumentStore();
    }
}