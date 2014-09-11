using System;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OpenDeploymentManager.Server.Host.Models.Entity;

namespace OpenDeploymentManager.Server.Host.Security
{
    public class ApiKeyAuthenticationOptions : AuthenticationOptions
    {
        private readonly Func<UserManager<ApplicationUser>> userManagerFactory;

        public ApiKeyAuthenticationOptions(Func<UserManager<ApplicationUser>> userManagerFactory)
            : base(ApiKeyConstants.AuthenticationType)
        {
            this.userManagerFactory = userManagerFactory;
        }

        public Func<UserManager<ApplicationUser>> UserManagerFactory
        {
            get
            {
                return this.userManagerFactory;
            }
        }
    }
}
