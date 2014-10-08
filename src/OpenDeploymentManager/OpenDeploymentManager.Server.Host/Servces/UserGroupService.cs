using System;
using System.Linq;
using OpenDeploymentManager.Common.Annotations;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using Raven.Client;

namespace OpenDeploymentManager.Server.Host.Servces
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IDocumentSession session;

        public UserGroupService([NotNull] IDocumentSession session)
        {
            this.session = session.ArgumentNotNull("session");
        }

        #region Implementation of IUserGroupService
        public IQueryable<ApplicationUserGroup> Query()
        {
            return this.session.Query<ApplicationUserGroup>();
        }

        public ApplicationUserGroup GetById(Guid id)
        {
            return this.session.Load<ApplicationUserGroup>(id);
        }

        public void Create(ApplicationUserGroup userGroup)
        {
            userGroup.ArgumentNotNull("userGroup");

            this.session.AreConstraintsFree(userGroup);
            this.session.Store(userGroup);
        }

        public void Update(ApplicationUserGroup userGroup)
        {
            userGroup.ArgumentNotNull("userGroup");
            this.session.AreConstraintsFree(userGroup);
        }

        public void Delete(ApplicationUserGroup userGroup)
        {
            userGroup.ArgumentNotNull("userGroup");
            this.session.Delete(userGroup);
        }
        #endregion
    }
}