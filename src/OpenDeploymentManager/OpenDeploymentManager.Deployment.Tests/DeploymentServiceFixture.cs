using System.IO;
using Moq;
using NUnit.Framework;
using OpenDeploymentManager.Deployment.Activities;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment.Tests
{
    [TestFixture]
    public class DeploymentServiceFixture
    {
        [Test]
        public void RunDeployment_WithOneActivity_ExecutesActivity()
        {
            // arrange
            string template = this.LoadTemplate("OpenDeploymentManager.Deployment.Tests.TestData.SimpleWorkflow.xaml");

            var variablesExtension = new Mock<IDeploymentVariablesExtension>();

            var factory = new Mock<IDeploymentExtensionsFactory>();
            factory.Setup(f => f.CreateExtension<IDeploymentVariablesExtension>()).Returns(variablesExtension.Object);
            factory.Setup(f => f.CreateExtension<IDeploymentLoggingExtension>()).Returns(new Mock<IDeploymentLoggingExtension>().Object);

            var target = new DeploymentService(factory.Object);

            // act
            target.RunDeployment(template, null);

            // assert
            variablesExtension.Verify(e => e.Get<string>("test"));
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
