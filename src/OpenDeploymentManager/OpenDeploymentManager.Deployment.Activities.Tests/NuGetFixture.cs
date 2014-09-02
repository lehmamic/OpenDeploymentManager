using System.Collections.Generic;
using System.IO;
using Microsoft.Activities.UnitTesting;
using NUnit.Framework;

namespace OpenDeploymentManager.Deployment.Activities.Tests
{
    [TestFixture]
    public class NuGetFixture
    {
        [Test]
        public void Execute_CallInstallCommand_InstallsNuGetPackage()
        {
            // arrange
            TestHelper.DeleteDirectoryIfExists("NuGetInstallTest");

            var target = new NuGet();
            WorkflowInvokerTest invoker = TestHelper.CreateWorkflowInvoker(target);

            // act
            var parameters = new Dictionary<string, object>
            {
                { "Command", NuGetCommand.Install },
                { "SourceRepository", new DirectoryInfo("TestData").FullName },
                { "LocalPath", "NuGetInstallTest" },
                { "PackageId", "MyPackage" },
                { "PackageVersion", "1.0.0" },
            };

            invoker.TestActivity(parameters);

            // assert
            invoker.AssertOutArgument.AreEqual("PackageInstallationPath", @"NuGetInstallTest\MyPackage.1.0.0");
            Assert.That(Directory.Exists(@"NuGetInstallTest\MyPackage.1.0.0"), Is.True);
        }
    }
}
