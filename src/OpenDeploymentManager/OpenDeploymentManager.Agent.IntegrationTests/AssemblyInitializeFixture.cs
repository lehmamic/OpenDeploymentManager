// must be outside of a namespace so that it initializes the assembly.
using NUnit.Framework;
using OpenDeploymentManager.Agent.IntegrationTests;

[SetUpFixture]
// ReSharper disable once CheckNamespace
public class AssemblySetUpFixture
{
    private readonly AgentHost host = new AgentHost();

    [SetUp]
    public void AssemblySetup()
    {
        this.host.StartAgent();
    }

    ////[TearDown]
    ////public void AssemblyTearDown()
    ////{
    ////    this.host.StopAgent();
    ////}
}
