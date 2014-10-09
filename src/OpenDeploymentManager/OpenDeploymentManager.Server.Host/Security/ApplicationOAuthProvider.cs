using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Security
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Coming from the original template.")]
        public ApplicationOAuthProvider(string publicClientId)
        {
            this.publicClientId = publicClientId.ArgumentNotNull("publicClientId");
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
                                                   {
                                                       { "userName", userName }
                                                   };

            return new AuthenticationProperties(data);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity authIdentity = await userManager.CreateIdentityAsync(user, context.Options.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(user.UserName);

            var ticket = new AuthenticationTicket(authIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            context.ArgumentNotNull("context");

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.ArgumentNotNull("context");

            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.ArgumentNotNull("context");

            if (context.ClientId == this.publicClientId)
            {
                var expectedRootUri = new Uri(context.Request.Uri, new Uri("/"));
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}