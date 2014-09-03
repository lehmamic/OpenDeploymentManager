using System.Activities;
using System.IO;

namespace OpenDeploymentManager.Agent.IntegrationTests.Workflows
{
    public class CreateFile : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> FileName { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var directory = new DirectoryInfo(@"SimpleWorkflowTest");
            if (!directory.Exists)
            {
                directory.Create();
            }

            File.Create(this.FileName.Get<string>(context));
        }
    }
}