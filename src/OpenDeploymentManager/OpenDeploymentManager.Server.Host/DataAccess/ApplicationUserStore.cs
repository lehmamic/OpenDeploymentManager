using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Server.Host.Models.Entity;
using Raven.Client;
using RavenDB.AspNet.Identity;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class ApplicationUserStore : UserStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>
    {
        private readonly Func<IDocumentSession> getSession;

        public ApplicationUserStore(Func<IDocumentSession> getSession)
            : base(getSession)
        {
            this.getSession = getSession;
        }

        public ApplicationUserStore(IDocumentSession session)
            : base(session)
        {
            this.getSession = () => session;
        }

        #region Implementation of IQueryableUserStore<ApplicationUser,in string>
        public IQueryable<ApplicationUser> Users
        {
            get
            {
                return this.getSession().Query<ApplicationUser>();
            }
        }
        #endregion
    }
}
