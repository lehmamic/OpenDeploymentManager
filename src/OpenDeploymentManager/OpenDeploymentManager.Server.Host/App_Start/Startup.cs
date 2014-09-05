﻿using System;
using JetBrains.Annotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Providers;
using Owin;
using PiranhaDeploy.Server.Api.Identity.Providers;

namespace OpenDeploymentManager.Server.Host
{
    public static class Startup
    {
        public const string PublicClientId = "self";

        private static OAuthAuthorizationServerOptions authorizationServerOptions;

        public static OAuthAuthorizationServerOptions OAuthOptions
        {
            get
            {
                return authorizationServerOptions ?? (authorizationServerOptions = CreateAuthorizationOptions());
            }
        }

        public static void Configuration(IAppBuilder app, IUnityContainer container)
        {
            app.ArgumentNotNull("app");
            container.ArgumentNotNull("container");

            ConfigureAuth(app);

            GlobalConfiguration.Configure(c => UnityConfig.Register(c, container));
            GlobalConfiguration.Configure(WebApiConfig.Register);
            app.Properties["host.AppName"] = "Open Deployment Manager";
            app.UseWebApi(GlobalConfiguration.Configuration);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers

            ////app.UseMicrosoftAccountAuthentication(
            ////    clientId: "",
            ////    clientSecret: "");

            ////app.UseTwitterAuthentication(
            ////    consumerKey: "",
            ////    consumerSecret: "");

            ////app.UseFacebookAuthentication(
            ////    appId: "",
            ////    appSecret: "");

            ////app.UseGoogleAuthentication();
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        private static OAuthAuthorizationServerOptions CreateAuthorizationOptions()
        {
            Func<UserManager<ApplicationUser>> userManagerFactory = () => GlobalConfiguration.Configuration.DependencyResolver.Resolve<UserManager<ApplicationUser>>();

            return new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, userManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }
    }
}