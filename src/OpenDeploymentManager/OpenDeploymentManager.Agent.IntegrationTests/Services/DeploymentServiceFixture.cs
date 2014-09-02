using System;
using NUnit.Framework;
using OpenDeploymentManager.Agent.Client;
using OpenDeploymentManager.Agent.Contracts;

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
            IDeploymentAgent agent = connectionManager.Discover(new Uri("net.tcp://localhost:53714/"));

            // act
            var target = agent.GetService<IDeploymentService>();

            // assert
            Assert.That(target, Is.Not.Null);
        }

        [Test]
        [Ignore]
        public void Deploy_SimpleDeploymentWorkflow_ExecutesWorkflow()
        {
            // arrange
            var connectionManager = new AgentConnectionManager();
            IDeploymentAgent agent = connectionManager.Discover(new Uri("net.tcp://localhost:53714/"));
            var target = agent.GetService<IDeploymentService>();

            // act
            target.Deploy(Guid.NewGuid(), Guid.NewGuid(), "", new KeyValue<object>[0], new KeyValue<string>[0]);

            // assert
            Assert.That(target, Is.Not.Null);
        }
    }
}
