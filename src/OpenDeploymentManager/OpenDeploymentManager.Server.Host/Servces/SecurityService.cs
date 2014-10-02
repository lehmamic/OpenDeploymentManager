using OpenDeploymentManager.Common.Diagnostics;
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
    }
}
