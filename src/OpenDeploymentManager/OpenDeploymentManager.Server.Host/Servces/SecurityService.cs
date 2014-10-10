using System;
using System.Collections.Generic;
using System.Linq;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Security;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public class SecurityService : ISecurityService
    {
        private readonly IDocumentSession session;

        public SecurityService(IDocumentSession session)
        {
            this.session = session.ArgumentNotNull("session");
        }

        #region Implementation of ISecurityService
        public IEnumerable<GlobalResources> GetGlobalResources()
        {
            return Enum.GetValues(typeof(GlobalResources)).Cast<GlobalResources>();
        }

        public IEnumerable<ResourceOperations> GetResourceOperations(GlobalResources resource)
        {
            return new[]
                       {
                           ResourceOperations.Create,
                           ResourceOperations.Delete,
                           ResourceOperations.Read,
                           ResourceOperations.ReadAll,
                           ResourceOperations.Update,
                       };
        }
        #endregion
    }
}
