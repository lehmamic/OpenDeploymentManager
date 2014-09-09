// must be outside of a namespace so that it initializes the assembly.
using NUnit.Framework;
using OpenDeploymentManager.Server.IntegrationTests;

[SetUpFixture]
// ReSharper disable once CheckNamespace
public class AssemblySetUpFixture
{
    private readonly AgentHost agent = new AgentHost();
    private readonly ServerHost server = new ServerHost();

    [SetUp]
    public void AssemblySetup()
    {
        this.server.StartAgent();
    }

    ////[TearDown]
    ////public void AssemblyTearDown()
    ////{
    ////    this.host.StopAgent();
    ////}
}
