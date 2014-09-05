﻿using System;
using Raven.Client;
using Raven.Client.Embedded;

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

        public IDocumentStore CreateDocumentStore()
        {
            var documentStore = new EmbeddableDocumentStore { ConnectionStringName = this.connectionStringName };
            documentStore.Initialize();

            return documentStore;
        }
    }
}