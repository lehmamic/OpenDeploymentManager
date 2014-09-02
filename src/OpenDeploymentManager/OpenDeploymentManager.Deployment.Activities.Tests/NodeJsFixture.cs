using System.Collections.Generic;
using System.IO;
using Microsoft.Activities.UnitTesting;
using NUnit.Framework;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment.Activities.Tests
{
    [TestFixture]
    public class NodeJsFixture
    {
        [Test]
        public void Execute_CallNpmFileWithInstallCommand_ExecutesJavaScriptFile()
        {
            // arrange
            TestHelper.DeleteDirectoryIfExists("nodetest");
            TestHelper.CopyFile(@"TestData\package.json", @"nodetest\package.json");

            var target = new NodeJs();
            WorkflowInvokerTest invoker = TestHelper.CreateWorkflowInvoker(target);

            var nodeExe = new FileInfo("Node.exe".FindExePath());
            var npmJavaScriptFile = Path.Combine(nodeExe.DirectoryName, @"node_modules\npm\bin\npm-cli.js");

            // act
            var parameters = new Dictionary<string, object>
            {
                { "WorkingDirectory", "nodetest" },
                { "JavaScriptFile", npmJavaScriptFile },
                { "ApplicationParameters", "install" },
            };

            invoker.TestActivity(parameters);

            // assert
            Assert.IsTrue(Directory.Exists(@"nodetest\node_modules"));
        }
    }
}
