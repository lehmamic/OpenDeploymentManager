using System;
using AutoMapper;
using Bootstrap.Unity;
using Microsoft.Practices.Unity;
using NLog;
using OpenDeploymentManager.Agent.Contracts;
using OpenDeploymentManager.Agent.Host.Properties;
using OpenDeploymentManager.Agent.Host.Services;
using OpenDeploymentManager.Common.Projection;
using OpenDeploymentManager.Common.Unity;
using OpenDeploymentManager.Deployment;

namespace OpenDeploymentManager.Agent.Host
{
    public class ContainerRegistration : IUnityRegistration
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

            container.RegisterTypeAsSingleton<IDeploymentManager, DeploymentManager>();

            container.RegisterTypePerRequest<IAgentInfoService, DeploymentAgentService>();
            container.RegisterTypePerRequest<IDeploymentService, DeploymentAgentService>();

            Log.Trace(Resources.InitializeContainerTask_InitializeProjection);
            Mapper.AssertConfigurationIsValid();

            var factory = new AutoMapperTypeAdapterFactory();
            TypeAdapterFactory.SetCurrent(factory);
        }
        #endregion
    }
}
