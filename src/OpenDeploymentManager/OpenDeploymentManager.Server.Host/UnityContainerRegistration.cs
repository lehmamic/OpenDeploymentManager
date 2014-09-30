﻿using AutoMapper;
using Bootstrap.Unity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NLog;
using OpenDeploymentManager.Common.Diagnostics;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Common.Unity;
using OpenDeploymentManager.Server.Host.DataAccess;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Properties;
using OpenDeploymentManager.Server.Host.Servces;
using Raven.Client;
using RavenDB.AspNet.Identity;

namespace OpenDeploymentManager.Server.Host
{
    public class UnityContainerRegistration : IUnityRegistration
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Implementation of IUnityRegistration
        public void Register(IUnityContainer container)
        {
            container.ArgumentNotNull("container");

            Log.Trace(Resources.InitializeContainerTask_ConfiguringContainer);
            container.AddNewExtension<Interception>();

            // register db
            container.RegisterTypeAsSingleton<IDocumentStoreFactory, DocumentStoreFactory>(
                new InjectionConstructor());
            container.RegisterTypeAsSingleton<IDocumentStore>(c => c.Resolve<IDocumentStoreFactory>().CreateDocumentStore());
            container.RegisterTypePerRequest<IDocumentSession>(c => c.Resolve<IDocumentStore>().OpenSession());

            // register asp identity
            container.RegisterTypePerRequest<IUserStore<ApplicationUser>, ApplicationUserStore>(
                new InjectionConstructor(new ResolvedParameter<IDocumentSession>()));

            container.RegisterTypeAsSingleton<ISecureDataFormat<AuthenticationTicket>>(
                c => Startup.OAuthOptions.AccessTokenFormat);

            // register services
            container.RegisterTypePerRequest<IUserService, UserService>(
                new InterceptionBehavior<PolicyInjectionBehavior>(),
                new Interceptor<InterfaceInterceptor>());

            ////container.RegisterTypeAsSingleton<IDeploymentManager, DeploymentManager>();

            ////container.RegisterTypePerRequest<IAgentInfoService, DeploymentAgentService>(
            ////    new InterceptionBehavior<PolicyInjectionBehavior>(),
            ////    new Interceptor<InterfaceInterceptor>());

            ////container.RegisterTypePerRequest<IDeploymentService, DeploymentAgentService>(
            ////    new InterceptionBehavior<PolicyInjectionBehavior>(),
            ////    new Interceptor<InterfaceInterceptor>());

            Log.Trace(Resources.InitializeContainerTask_InitializeServiceLocator);
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            container.RegisterInstance(ServiceLocator.Current);

            Log.Trace(Resources.InitializeContainerTask_InitializeProjection);
            Mapper.AssertConfigurationIsValid();

            var factory = new AutoMapperTypeAdapterFactory();
            TypeAdapterFactory.SetCurrent(factory);
        }
        #endregion
    }
}
