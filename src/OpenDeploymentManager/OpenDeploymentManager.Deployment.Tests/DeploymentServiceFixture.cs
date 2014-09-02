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
            target.RunDeployment(template, null);

            // assert
            Assert.That(File.Exists(@"SimpleWorkflowTest\TestFile.txt"), Is.True);
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
