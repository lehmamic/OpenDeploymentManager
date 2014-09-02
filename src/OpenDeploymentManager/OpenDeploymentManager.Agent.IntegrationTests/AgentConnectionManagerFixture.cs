using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using NUnit.Framework;
using OpenDeploymentManager.Agent.Client;
using OpenDeploymentManager.Agent.Host;

namespace OpenDeploymentManager.Agent.IntegrationTests
{
    [TestFixture]
    public class AgentConnectionManagerFixture
    {
        [Test]
        public void Discover_AllAgents_ReturnsListWithAgent()
        {
            // arrange
            var connectionManager = new AgentConnectionManager();

            // act
            IEnumerable<IDeploymentAgent> discoveredAgents = connectionManager.Discover();

            // assert
            IDeploymentAgent agent = discoveredAgents.FirstOrDefault();
            Assert.That(agent, Is.Not.Null);
            Assert.That(agent.IsAlive(), Is.True);
            Assert.That(agent.Uri, Is.EqualTo(new Uri("net.tcp://localhost:53714/")));
            Assert.That(agent.MachineName, Is.EqualTo(Environment.MachineName));
            Assert.That(agent.Version, Is.EqualTo(typeof(Program).Assembly.GetName().Version));
        }

        [Test]
        public void Discover_SingleAgent_ReturnsAgent()
        {
            // arrange
            var agentUri = new Uri("net.tcp://localhost:53714/");
            var connectionManager = new AgentConnectionManager();

            // act
            IDeploymentAgent agent = connectionManager.Discover(agentUri);

            // assert
            Assert.That(agent.IsAlive(), Is.True);
            Assert.That(agent.Uri, Is.EqualTo(agentUri));
            Assert.That(agent.MachineName, Is.EqualTo(Environment.MachineName));
            Assert.That(agent.Version, Is.EqualTo(typeof(Program).Assembly.GetName().Version));
        }

        [Test]
        [ExpectedException(typeof(CommunicationException))]
        public void Discover_SingleAgentNotAvailable_ThrowsException()
        {
            // arrange
            var agentUri = new Uri("net.tcp://someuri:53714/");
            var connectionManager = new AgentConnectionManager();

            // act
            IDeploymentAgent agent = connectionManager.Discover(agentUri);
        }
    }
}
