using System;
using System.Globalization;
using AutoMapper;
using Bootstrap.Extensions.StartupTasks;
using Microsoft.Practices.Unity;
using NLog;
using OpenDeploymentManager.Agent.Contracts;
using OpenDeploymentManager.Agent.Host.Properties;
using OpenDeploymentManager.Agent.Host.Services;
using OpenDeploymentManager.Core.Common.Projection;
using OpenDeploymentManager.Core.Common.Unity;

namespace OpenDeploymentManager.Agent.Host.StartupTasks
{
    public class InitializeContainerTask : IStartupTask
    {
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IUnityContainer container;

        public InitializeContainerTask(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        #region Implementation of IStartupTask
        public void Run()
        {
            Log.Trace(Resources.InitializeContainerTask_ConfiguringContainer);

            this.container.RegisterTypePerRequest<IAgentInfoService, DeploymentAgentService>();

            Mapper.AssertConfigurationIsValid();

            var factory = new AutoMapperTypeAdapterFactory();
            TypeAdapterFactory.SetCurrent(factory);
        }

        public void Reset()
        {
        }
        #endregion
    }
}
