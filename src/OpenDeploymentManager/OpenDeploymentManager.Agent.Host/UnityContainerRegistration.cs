using System;
using AutoMapper;
using Bootstrap.Unity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NLog;
using OpenDeploymentManager.Agent.Contracts;
using OpenDeploymentManager.Agent.Host.Properties;
using OpenDeploymentManager.Agent.Host.Services;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Common.Unity;
using OpenDeploymentManager.Deployment;

namespace OpenDeploymentManager.Agent.Host
{
    public class UnityContainerRegistration : IUnityRegistration
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Implementation of IUnityRegistration
        public void Register(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Log.Trace(Resources.InitializeContainerTask_ConfiguringContainer);
            container.AddNewExtension<Interception>();

            container.RegisterTypeAsSingleton<IDeploymentManager, DeploymentManager>();

            container.RegisterTypePerRequest<IAgentInfoService, DeploymentAgentService>(
                new InterceptionBehavior<PolicyInjectionBehavior>(),
                new Interceptor<InterfaceInterceptor>());

            container.RegisterTypePerRequest<IDeploymentService, DeploymentAgentService>(
                new InterceptionBehavior<PolicyInjectionBehavior>(),
                new Interceptor<InterfaceInterceptor>());

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
