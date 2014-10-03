using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Theading;
using OpenDeploymentManager.Server.Host.Models.Entity;
using Raven.Client;
using Raven.Client.Document;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class ApplicationRoleStore : IRoleStore<ApplicationRole>, IQueryableRoleStore<ApplicationRole>
    {
        private readonly IDocumentSession session;
        private bool disposed;

        public ApplicationRoleStore(IDocumentSession session)
        {
            this.session = session.ArgumentNotNull("session");
        }

        #region Implementation of IQueryableRoleStore<ApplicationRole,in string>
        public IQueryable<ApplicationRole> Roles
        {
            get
            {
                this.ThrowIfDisposed();

                return this.session.Query<ApplicationRole>();
            }
        }
        #endregion

        #region Implementation of IRoleStore<ApplicationRole,in Guid>
        public Task CreateAsync(ApplicationRole role)
        {
            this.ThrowIfDisposed();

            role.ArgumentNotNull("role");

            role.Id = this.RolenameToDocumentId(role.Name);
            return Task.Run(() => this.session.Store(role));
        }

        public Task UpdateAsync(ApplicationRole role)
        {
            this.ThrowIfDisposed();

            return AsyncHelper.Void;
        }

        public Task DeleteAsync(ApplicationRole role)
        {
            this.ThrowIfDisposed();

            role.ArgumentNotNull("role");

            return Task.Run(() => this.session.Delete(role));
        }

        public Task<ApplicationRole> FindByIdAsync(string roleId)
        {
            this.ThrowIfDisposed();

            return Task.Run(() => this.session.Load<ApplicationRole>(roleId));
        }

        public Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            this.ThrowIfDisposed();

            return this.FindByIdAsync(this.RolenameToDocumentId(roleName));
        }
        #endregion

        #region Implementation of IDisposable
        public void Dispose()
        {
            this.disposed = true;
        }
        #endregion

        private void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }

        private string RolenameToDocumentId(string userName)
        {
            DocumentConvention conventions = this.session.Advanced.DocumentStore.Conventions;
            string typeTagName = conventions.GetTypeTagName(typeof(ApplicationRole));
            return string.Format("{0}{1}{2}", (object)conventions.TransformTypeTagNameToDocumentKeyPrefix(typeTagName), (object)conventions.IdentityPartsSeparator, (object)userName);
        }
    }
}