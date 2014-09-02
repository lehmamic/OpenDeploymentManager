using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenDeploymentManager.Deployment
{
    public static class DeploymentManagerExtensions
    {
        public static Task RunDeploymentAsync(IDeploymentManager deploymentManager, string template, IDictionary<string, object> arguments, IDictionary<string, string> variables)
        {
            if (deploymentManager == null)
            {
                throw new ArgumentNullException("deploymentManager");
            }

            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            if (variables == null)
            {
                throw new ArgumentNullException("variables");
            }

            return Task.Run(() => deploymentManager.RunDeployment(template, arguments, variables));
        }
    }
}