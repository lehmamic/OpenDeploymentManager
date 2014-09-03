using System;
using System.IO;
using NUnit.Framework;
using OpenDeploymentManager.Agent.Client;
using OpenDeploymentManager.Agent.Contracts;
using OpenDeploymentManager.Agent.Host;

namespace OpenDeploymentManager.Agent.IntegrationTests.Services
{
    [TestFixture]
    public class DeploymentServiceFixture
    {
        [Test]
        public void GetService_AgentIsAlive_ReturnsDeploymentService()
        {
            // arrange
            var connectionManager = new AgentConnectionManager();
            IDeploymentAgent agent = connectionManager.Discover(new Uri(AgentConfiguration.AgentUrl));

            // act
            var target = agent.GetService<IDeploymentService>();

            // assert
            Assert.That(target, Is.Not.Null);
        }

        [Test]
        public void Deploy_SimpleWorkflow_ExecutesWorkflow()
        {
            // arrange
            string expectedFileName = Path.Combine("SimpleWorkflowTest", "TestFile.txt");
            string template = this.LoadTemplate("OpenDeploymentManager.Agent.IntegrationTests.Workflows.SimpleWorkflow.xaml");

            var connectionManager = new AgentConnectionManager();
            IDeploymentAgent agent = connectionManager.Discover(new Uri(AgentConfiguration.AgentUrl));
            var target = agent.GetService<IDeploymentService>();

            // act
            var arguments = new[] { new KeyValue<object> { Key = "FileName", Value = expectedFileName } };
            target.Deploy(Guid.NewGuid(), Guid.NewGuid(), template, arguments, new KeyValue<string>[0]);

            // assert
            Assert.That(File.Exists(expectedFileName), Is.True);
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
