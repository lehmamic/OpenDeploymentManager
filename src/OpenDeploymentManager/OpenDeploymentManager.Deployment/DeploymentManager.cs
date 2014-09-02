using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment
{
    public class DeploymentManager : IDeploymentManager
    {
        private readonly IServiceLocator serviceLocator;

        public DeploymentManager(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }

            this.serviceLocator = serviceLocator;
        }

        public void RunDeployment(string template, object variables)
        {
            Activity workflow = LoadWorkflow(template);

            var workflowInvoker = new WorkflowInvoker(workflow);
            workflowInvoker.Extensions.Add(this.serviceLocator);
            workflowInvoker.Extensions.Add(new ActivityTracking(new DeploymentInformationNode(Guid.Empty, DateTimeOffset.Now, null, "Executing deployment worflow.")));
            workflowInvoker.Extensions.Add(new DeploymentVariablesExtension(new Dictionary<string, object>()));

            var workflowInputs = new Dictionary<string, object>();
            workflowInvoker.Invoke(workflowInputs);
        }

        private static Activity LoadWorkflow(string template)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(template)))
            {
                return ActivityXamlServices.Load(stream);
            }
        }
    }
}