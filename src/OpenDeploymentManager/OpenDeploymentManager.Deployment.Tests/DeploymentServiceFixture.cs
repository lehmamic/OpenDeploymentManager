using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.ServiceLocation;
using Moq;
using NUnit.Framework;

namespace OpenDeploymentManager.Deployment.Tests
{
    [TestFixture]
    public class DeploymentServiceFixture
    {
        [Test]
        public void RunDeployment_WithOneActivity_ExecutesActivity()
        {
            // arrange
            var directory = new DirectoryInfo("SimpleWorkflowTest");
            if (directory.Exists)
            {
                directory.Delete(true);
            }

            string template = this.LoadTemplate("OpenDeploymentManager.Deployment.Tests.TestData.SimpleWorkflow.xaml");

            var factory = new Mock<IServiceLocator>();
            var target = new DeploymentManager(factory.Object);

            // act
            target.RunDeployment(template, new Dictionary<string, object>(), new Dictionary<string, string>());

            // assert
            Assert.That(File.Exists(@"SimpleWorkflowTest\TestFile.txt"), Is.True);
        }

        [Test]
        public void RunDeployment_SetWorkflowArguments_ExecutesWorkflowWithArguments()
        {
            // arrange
            var directory = new DirectoryInfo("SimpleWorkflowTest");
            if (directory.Exists)
            {
                directory.Delete(true);
            }

            string template = this.LoadTemplate("OpenDeploymentManager.Deployment.Tests.TestData.WorkflowAssertVariable.xaml");

            var factory = new Mock<IServiceLocator>();
            var target = new DeploymentManager(factory.Object);

            // act
            var dictionary = new Dictionary<string, object>() { { "TargetVariable", "ValueWhichIsNotNull" } };
            Assert.DoesNotThrow(() => target.RunDeployment(template, dictionary, new Dictionary<string, string>()));
        }

        [Test]
        public void RunDeployment_DontSetWorkflowArguments_ThrowsException()
        {
            // arrange
            var directory = new DirectoryInfo("SimpleWorkflowTest");
            if (directory.Exists)
            {
                directory.Delete(true);
            }

            string template = this.LoadTemplate("OpenDeploymentManager.Deployment.Tests.TestData.WorkflowAssertVariable.xaml");

            var factory = new Mock<IServiceLocator>();
            var target = new DeploymentManager(factory.Object);

            // act
            var dictionary = new Dictionary<string, object>();
            Assert.Throws<ArgumentNullException>(() => target.RunDeployment(template, dictionary, new Dictionary<string, string>()));
        }

        private string LoadTemplate(string templateResource)
        {
            var stream = this.GetType().Assembly.GetManifestResourceStream(templateResource);
            try
            {
                using (var reader = new StreamReader(stream))
                {
                    stream = null;
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
    }
}
