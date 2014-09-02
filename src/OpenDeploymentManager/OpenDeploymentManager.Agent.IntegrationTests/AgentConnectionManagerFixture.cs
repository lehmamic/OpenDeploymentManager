using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenDeploymentManager.Agent.Client;
using OpenDeploymentManager.Agent.Host;

namespace OpenDeploymentManager.Agent.IntegrationTests
{
    [TestFixture]
    public class AgentConnectionManagerFixture
    {
        [Test]
        public void Discover_WithTestAgent_ReturnsAgent()
        {
            // arrange
            var connectionManager = new AgentConnectionManager();

            // act
            IEnumerable<IDeploymentAgent> discoveredAgents = connectionManager.Discover();

            // assert
            IDeploymentAgent agent = discoveredAgents.FirstOrDefault();
            Assert.That(agent, Is.Not.Null);
            Assert.That(agent.Uri, Is.EqualTo(new Uri("net.tcp://localhost:53714/")));
            Assert.That(agent.MachineName, Is.EqualTo(Environment.MachineName));
            Assert.That(agent.Version, Is.EqualTo(typeof(Program).Assembly.GetName().Version));
        }
    }
}
