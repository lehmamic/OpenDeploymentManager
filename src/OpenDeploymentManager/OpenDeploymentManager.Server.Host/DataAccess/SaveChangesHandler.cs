using Microsoft.Practices.Unity.InterceptionExtension;
using NLog;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Properties;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class SaveChangesHandler : ICallHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IDocumentSession session;

        public SaveChangesHandler(IDocumentSession session)
        {
            this.session = session.ArgumentNotNull("session");
        }

        #region Implementation of ICallHandler
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = getNext().Invoke(input, getNext);

            Log.Debug(Resources.SaveChangesHandler_SaveChanges);
            this.session.SaveChanges();

            return result;
        }
        #endregion
    }
}