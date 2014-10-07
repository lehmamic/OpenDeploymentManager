using System;
using OpenDeploymentManager.Common;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.UniqueConstraints;
using Raven.Database.Server;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class DocumentStoreFactory : IDocumentStoreFactory
    {
        private readonly string connectionStringName;

        public DocumentStoreFactory(string connectionStringName)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ArgumentException("A not empty connection string name is required.", "connectionStringName");
            }

            this.connectionStringName = connectionStringName;
        }

        public DocumentStoreFactory()
        {
            this.connectionStringName = string.Empty;
        }

        public IDocumentStore CreateDocumentStore()
        {
            var documentStore = !string.IsNullOrEmpty(this.connectionStringName) ? new EmbeddableDocumentStore { ConnectionStringName = this.connectionStringName } : new EmbeddableDocumentStore();

            documentStore.UseEmbeddedHttpServer = ServerConfiguration.UseEmbeddedHttpServer.ToBool(false);
            if (documentStore.UseEmbeddedHttpServer)
            {
                NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(ServerConfiguration.RavenDbPort.ToInt(0));
            }

            documentStore.RegisterListener(new UniqueConstraintsStoreListener());

            documentStore.Initialize();

            return documentStore;
        }
    }
}
