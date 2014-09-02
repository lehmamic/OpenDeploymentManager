using System;
using System.Activities;
using System.IO;
using Microsoft.Activities.UnitTesting;
using Moq;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment.Activities.Tests
{
    public static class TestHelper
    {
        public static WorkflowInvokerTest CreateWorkflowInvoker(Activity targetActivity)
        {
            var invoker = new WorkflowInvokerTest(targetActivity);
            invoker.Extensions.Add(CreateLoggingExtension());

            return invoker;
        }

        public static void DeleteDirectoryIfExists(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
        }

        public static void CopyFile(string sourceFile, string destFile)
        {
            var destFileInfo = new FileInfo(destFile);
            if (!destFileInfo.Directory.Exists)
            {
                destFileInfo.Directory.Create();
            }

            File.Copy(sourceFile, destFile);
        }

        private static IDeploymentLoggingExtension CreateLoggingExtension()
        {
            var loggingExtension = new Mock<IDeploymentLoggingExtension>();

            loggingExtension.Setup(l => l.GetActivityTracking(It.IsAny<ActivityContext>()))
                            .Returns(new ActivityTracking(new DeploymentInformationNode(Guid.NewGuid(), DateTimeOffset.Now, null, "Any Content")));

            return loggingExtension.Object;
        }
    }
}
