using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Theading;
using OpenDeploymentManager.Server.Host.Models.Entity;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class IdentityRoleStore<TRole, TKey> : IRoleStore<TRole, TKey>, IQueryableRoleStore<TRole, TKey>
        where TRole : IdentityRole<TKey>
        where TKey : struct
    {
        private readonly IDocumentSession session;
        private bool disposed;

        public IdentityRoleStore(IDocumentSession session)
        {
            this.session = session.ArgumentNotNull("session");
        }

        #region Implementation of IQueryableRoleStore<ApplicationRole,in string>
        public IQueryable<TRole> Roles
        {
            get
            {
                this.ThrowIfDisposed();

                return this.session.Query<TRole>();
            }
        }
        #endregion

        #region Implementation of IRoleStore<ApplicationRole,in Guid>
        public Task CreateAsync(TRole role)
        {
            this.ThrowIfDisposed();

            role.ArgumentNotNull("role");

            return Task.Run(() => this.session.Store(role));
        }

        public Task UpdateAsync(TRole role)
        {
            this.ThrowIfDisposed();

            return AsyncHelper.Void;
        }

        public Task DeleteAsync(TRole role)
        {
            this.ThrowIfDisposed();

            role.ArgumentNotNull("role");

            return Task.Run(() => this.session.Delete(role));
        }

        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            this.ThrowIfDisposed();

            return Task.Run(() => this.session.Load<TRole>(roleId));
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            this.ThrowIfDisposed();

            return Task.FromResult(this.session.LoadByUniqueConstraint<TRole>("Name", roleName));
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
    }
}