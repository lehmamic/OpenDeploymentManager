using System.Activities;
using OpenDeploymentManager.Deployment.Activities;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment.Tests
{
    public class SimpleCodeActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            var variables = context.GetExtension<IDeploymentVariablesExtension>();
            variables.Get<string>("test");
        }
    }
}