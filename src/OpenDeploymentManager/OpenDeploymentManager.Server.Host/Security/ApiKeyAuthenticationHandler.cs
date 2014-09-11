using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using OpenDeploymentManager.Server.Host.Helpers;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Security
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly ILogger logger;

        public ApiKeyAuthenticationHandler(ILogger logger)
        {
            this.logger = logger;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            // Find apiKey in default location
            string apiKey;
            if (!this.TryGetApiKey(out apiKey))
            {
                this.logger.WriteWarning("Api key not found");
                return new AuthenticationTicket(null, new AuthenticationProperties());
            }

            using (UserManager<ApplicationUser> userManager = this.Options.UserManagerFactory())
            {
                ApplicationUser user = userManager.Users.SingleOrDefault();
                if (user == null)
                {
                    //context.SetError("invalid_grant", "The user name or password is incorrect.");
                    //return;
                }

                ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
                this.Request.Context.Authentication.SignIn(cookiesIdentity);

                ClaimsIdentity authIdentity = await userManager.CreateIdentityAsync(user, this.Options.AuthenticationType);
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                return new AuthenticationTicket(authIdentity, properties);
            }
        }

        private bool TryGetApiKey(out string apiKey)
        {
            string authorization;

            if (this.Request.Headers.TryGetValue("ApiKey", out apiKey))
            {
                return true;
            }

            if (this.Request.Headers.TryGetValue("Authorization", out authorization))
            {
                if (authorization.StartsWith("ApiKey ", StringComparison.OrdinalIgnoreCase))
                {
                    apiKey = authorization.Substring("ApiKey ".Length).Trim();
                    return true;
                }
            }

            return false;
        }
    }
}