using System.Collections.Generic;
using System.IO;
using Microsoft.Activities.UnitTesting;
using NUnit.Framework;

namespace OpenDeploymentManager.Deployment.Activities.Tests
{
    [TestFixture]
    public class NpmFixture
    {
        [Test]
        public void Execute_CallInstallCommand_ExecutesNpmInstall()
        {
            // arrange
            TestHelper.DeleteDirectoryIfExists("npmtest");
            TestHelper.CopyFile(@"TestData\package.json", @"npmtest\package.json");

            var target = new Npm();
            WorkflowInvokerTest invoker = TestHelper.CreateWorkflowInvoker(target);

            // act
            var parameters = new Dictionary<string, object>
            {
                { "WorkingDirectory", "npmtest" },
                { "Command", "install" },
            };

            invoker.TestActivity(parameters);

            // assert
            Assert.IsTrue(Directory.Exists(@"npmtest\node_modules"));
        }
    }
}
