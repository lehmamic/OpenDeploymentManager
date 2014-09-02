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

        [Test]
        public void Execute_CallUpdateCommand_InstallsNuGetPackage()
        {
            // arrange
            TestHelper.DeleteDirectoryIfExists("NuGetUpdateTest");

            var target = new NuGet();
            WorkflowInvokerTest invoker = TestHelper.CreateWorkflowInvoker(target);

            var parameters = new Dictionary<string, object>
            {
                { "Command", NuGetCommand.Install },
                { "SourceRepository", new DirectoryInfo("TestData").FullName },
                { "LocalPath", "NuGetUpdateTest" },
                { "PackageId", "MyPackage" },
                { "PackageVersion", "1.0.0" },
            };

            invoker.TestActivity(parameters);

            // act
            parameters["Command"] = NuGetCommand.Update;
            parameters["PackageVersion"] = "1.1.0";

            invoker.TestActivity(parameters);

            // assert
            invoker.AssertOutArgument.AreEqual("PackageInstallationPath", @"NuGetUpdateTest\MyPackage.1.1.0");
            Assert.That(Directory.Exists(@"NuGetUpdateTest\MyPackage.1.1.0"), Is.True);
        }

        [Test]
        public void Execute_CallUninstallCommand_UninstallsNuGetPackage()
        {
            // arrange
            TestHelper.DeleteDirectoryIfExists("NuGetUnInstallTest");

            var target = new NuGet();
            WorkflowInvokerTest invoker = TestHelper.CreateWorkflowInvoker(target);

            var parameters = new Dictionary<string, object>
            {
                { "Command", NuGetCommand.Install },
                { "SourceRepository", new DirectoryInfo("TestData").FullName },
                { "LocalPath", "NuGetUnInstallTest" },
                { "PackageId", "MyPackage" },
                { "PackageVersion", "1.0.0" },
            };

            invoker.TestActivity(parameters);

            // act
            parameters["Command"] = NuGetCommand.Uninstall;
            parameters["PackageVersion"] = "1.0.0";

            invoker.TestActivity(parameters);

            // assert
            invoker.AssertOutArgument.AreEqual("PackageInstallationPath", @"NuGetUnInstallTest\MyPackage.1.0.0");
            Assert.That(Directory.Exists(@"NuGetUnInstallTest\MyPackage.1.0.0"), Is.False);
        }
    }
}
