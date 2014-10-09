using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Annotations;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Models.Entity;
using Raven.Client;
using Raven.Client.UniqueConstraints;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public class UserStore<TUser, TKey> : IUserLoginStore<TUser, TKey>, IUserClaimStore<TUser, TKey>, IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, IUserStore<TUser, TKey>, IQueryableUserStore<TUser, TKey>, IDisposable
        where TUser : IdentityUser<TKey>
        where TKey : struct
    {
        private readonly Func<IDocumentSession> getSessionFunc;

        private IDocumentSession session;
        private bool disposed;

        public UserStore(Func<IDocumentSession> getSession)
        {
            this.getSessionFunc = getSession;
        }

        public UserStore(IDocumentSession session)
        {
            this.session = session;
        }

        public IDocumentSession Session
        {
            get
            {
                return this.session ?? (this.session = this.getSessionFunc());
            }
        }

        #region Implementation of IQueryableUserStore<TUser,in TKey>
        public IQueryable<TUser> Users
        {
            get
            {
                return this.Session.Query<TUser>();
            }
        }
        #endregion

        #region Implementation of IUserStore<TUser, TKey>
        public Task CreateAsync(TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            this.Session.StoreUnique(user);
            return Task.FromResult(true);
        }

        public Task DeleteAsync(TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            this.Session.Delete(user);
            return Task.FromResult(true);
        }

        public Task<TUser> FindByIdAsync(TKey userId)
        {
            return Task.FromResult(this.Session.Load<TUser>(userId));
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(this.session.LoadByUniqueConstraint<TUser>("UserName", userName));
        }

        public Task UpdateAsync(TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            return Task.FromResult(true);
        }
        #endregion

        #region Implementation of IUserLoginStore<TUser, TKey>

        public Task AddLoginAsync(TUser user, [NotNull] UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");
            login.ArgumentNotNull("login");

            if (!user.Logins.Any(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey))
            {
                user.Logins.Add(login);
                this.Session.Store(
                    new IdentityUserLogin<TKey>
                        {
                            Id = login.GetLoginId(),
                            UserId = user.Id,
                            Provider = login.LoginProvider,
                            ProviderKey = login.ProviderKey
                        });
            }

            return Task.FromResult<bool>(true);
        }

        public Task<TUser> FindAsync([NotNull] UserLoginInfo login)
        {
            login.ArgumentNotNull("login");

            string loginId = login.GetLoginId();
            IdentityUserLogin<TKey> identityUserLogin =
                this.Session.Include<IdentityUserLogin<TKey>>(x => x.UserId).Load(loginId);
            TUser result = default(TUser);
            if (identityUserLogin != null)
            {
                result = this.Session.Load<TUser>(identityUserLogin.UserId);
            }

            return Task.FromResult(result);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync([NotNull] TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            IList<UserLoginInfo> logins = user.Logins.ToList();
            return Task.FromResult(logins);
        }

        public Task RemoveLoginAsync([NotNull] TUser user, [NotNull] UserLoginInfo login)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");
            login.ArgumentNotNull("login");

            var identityUserLogin = this.Session.Load<IdentityUserLogin<TKey>>(login.GetLoginId());
            if (identityUserLogin != null)
            {
                this.Session.Delete(identityUserLogin);
            }

            user.Logins.RemoveAll(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);

            return Task.FromResult<int>(0);
        }
        #endregion

        #region Implementation of  IUserClaimStore<TUser, TKey>
        public Task AddClaimAsync([NotNull] TUser user, [NotNull] Claim claim)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");
            claim.ArgumentNotNull("claim");

            if (!user.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
            {
                user.Claims.Add(new IdentityUserClaim() { ClaimType = claim.Type, ClaimValue = claim.Value });
            }

            return Task.FromResult(0);
        }

        public Task<IList<Claim>> GetClaimsAsync([NotNull] TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            IList<Claim> claims = user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(claims);
        }

        public Task RemoveClaimAsync([NotNull] TUser user, [NotNull] Claim claim)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");
            claim.ArgumentNotNull("claim");

            user.Claims.RemoveAll(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            return Task.FromResult(0);
        }
        #endregion

        #region Implementation IUserPasswordStore<TUser, TKey>
        public Task<string> GetPasswordHashAsync([NotNull] TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync([NotNull] TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync([NotNull] TUser user, string passwordHash)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region Implementation of IUserSecurityStampStore<TUser, TKey>
        public Task<string> GetSecurityStampAsync([NotNull] TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync([NotNull] TUser user, string stamp)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region Implementation of IUserRoleStore<TUser, TKey>
        public Task AddToRoleAsync([NotNull] TUser user, string role)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");
            if (!user.Roles.Contains(role, StringComparer.InvariantCultureIgnoreCase))
            {
                user.Roles.Add(role);
            }

            return Task.FromResult(0);
        }

        public Task<IList<string>> GetRolesAsync([NotNull] TUser user)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            return Task.FromResult((IList<string>)user.Roles);
        }

        public Task<bool> IsInRoleAsync([NotNull] TUser user, string role)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            return Task.FromResult(user.Roles.Contains(role, StringComparer.InvariantCultureIgnoreCase));
        }

        public Task RemoveFromRoleAsync([NotNull] TUser user, string role)
        {
            this.ThrowIfDisposed();
            user.ArgumentNotNull("user");

            user.Roles.RemoveAll(r => string.Equals(r, role, StringComparison.InvariantCultureIgnoreCase));

            return Task.FromResult(0);
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
