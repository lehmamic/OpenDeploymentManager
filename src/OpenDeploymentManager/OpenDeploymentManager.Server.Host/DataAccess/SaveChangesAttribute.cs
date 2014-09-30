using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SaveChangesAttribute : HandlerAttribute
    {
        private readonly int order;

        public SaveChangesAttribute()
            : this(0)
        {
        }

        public SaveChangesAttribute(int order)
        {
            this.order = order;
        }

        #region Overrides of HandlerAttribute
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new SaveChangesHandler(container.Resolve<IDocumentSession>()) { Order = this.order };
        }
        #endregion
    }
}
