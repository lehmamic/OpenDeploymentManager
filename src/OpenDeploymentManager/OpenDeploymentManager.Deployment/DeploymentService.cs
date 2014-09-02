using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenDeploymentManager.Deployment.Activities;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment
{
    public class DeploymentService
    {
        private readonly IDeploymentExtensionsFactory extensionFactory;

        public DeploymentService(IDeploymentExtensionsFactory extensionFactory)
        {
            if (extensionFactory == null)
            {
                throw new ArgumentNullException("extensionFactory");
            }

            this.extensionFactory = extensionFactory;
        }

        public void RunDeployment(string template, object variables)
        {
            Activity workflow = LoadWorkflow(template);

            var workflowInvoker = new WorkflowInvoker(workflow);
            workflowInvoker.Extensions.Add(this.extensionFactory.CreateExtension<IDeploymentLoggingExtension>);
            workflowInvoker.Extensions.Add(this.extensionFactory.CreateExtension<IDeploymentVariablesExtension>);

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