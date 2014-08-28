using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDeploymentManager.Deployment
{
    public class Deployment
    {
        public void RunDeployment(string template, object variables)
        {
            Activity workflow = LoadWorkflow(template);

            var workflowInvoker = new WorkflowInvoker(workflow);

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