using System.Activities;
using System.IO;

namespace OpenDeploymentManager.Deployment.Tests
{
    public class SimpleCodeActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            var directory = new DirectoryInfo(@"SimpleWorkflowTest");
            if (!directory.Exists)
            {
                directory.Create();
            }

            File.Create(@"SimpleWorkflowTest\TestFile.txt");
        }
    }
}