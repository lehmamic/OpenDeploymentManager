using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using OpenDeploymentManager.Deployment.Activities.Services;

namespace OpenDeploymentManager.Deployment.Activities.Tests.Node
{
    [TestFixture]
    public class NpmCliTest
    {
        [Test]
        public void Run_WithHelpCommand_CanExecuteCommand()
        {
            // arrange
            var output = new StringBuilder();

            var target = new NpmCli(Constants.DefaultNodeExePath);
            target.OutputDataReceived += (sender, e) =>
                {
                    output.AppendLine(e.Data);
                    Console.WriteLine(e.Data);
                };

            // act
            target.Run("help");

            // assert
            Assert.IsTrue(output.ToString().Contains("Usage: npm <command>"));
        }

        [Test]
        public void Run_WithInstallCommandInSpecificWorkingDirectory_ModuleInstalledinWorkingDirectory()
        {
            // arrange
            var workingDirectory = new DirectoryInfo("NpmTest");
            workingDirectory.Create();

            var target = new NpmCli(Constants.DefaultNodeExePath);
            target.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);

            // act
            target.WorkingDirectory = workingDirectory.FullName;
            target.Run("install underscore");

            // assert
            Assert.IsTrue(Directory.Exists(Path.Combine(workingDirectory.FullName, "node_modules", "underscore")));
        }
    }
}