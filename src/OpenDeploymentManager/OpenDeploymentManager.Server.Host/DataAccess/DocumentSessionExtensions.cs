using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenDeploymentManager.Common;
using OpenDeploymentManager.Common.Annotations;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Reflection;
using Raven.Client;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class DocumentSessionExtensions
    {
        public static void AreConstraintsFree<TDocument>([NotNull] this IDocumentSession session, TDocument document)
        {
            UniqueConstraintCheckResult<TDocument> checkResult = session.CheckForUniqueConstraints(document);

            // returns whether its constraints are available
            if (!checkResult.ConstraintsAreFree())
            {
                IEnumerable<PropertyInfo> uniqueConstraintProperties = typeof(TDocument).GetProperties().Where(p => p.IsDefined<UniqueConstraintAttribute>(false));
                throw new UniqueConstraintException("The unique constraints for type {0} are violated.".Invariant(typeof(TDocument)), uniqueConstraintProperties);
            }
        }

        public static void SaveChanges([NotNull] this IAsyncDocumentSession session)
        {
            session.ArgumentNotNull("session");

            session.SaveChangesAsync().Wait();
        }
    }
}