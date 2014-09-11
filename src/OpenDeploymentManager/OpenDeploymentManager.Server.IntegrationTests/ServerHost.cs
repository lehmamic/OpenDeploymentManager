﻿using OpenDeploymentManager.Server.Host;

namespace OpenDeploymentManager.Server.IntegrationTests
{
    public class ServerHost : Program
    {
        public void StartAgent()
        {
            this.OnStart(new string[0]);
        }

        public void StopAgent()
        {
            this.StopAgent();
        }
    }
}