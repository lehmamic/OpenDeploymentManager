using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Common;
using OpenDeploymentManager.Server.Host;

namespace OpenDeploymentManager.Server.IntegrationTests.Authentication
{
    [TestFixture]
    public class BearerTokenAuthenticationFixture
    {
        [Test]
        public void Authenticate_WithValidCredentials_ReturnsTokenHeader()
        {
            // arrange
            var target = new BearerTokenAuthentication("admin", "123456");
            var endpoint = new OpenDeploymentManagerEndpoint(ServerConfiguration.ServerUrl.ToUri(), target);

            // act
            AuthenticationHeaderValue result = target.Authenticate(endpoint.UriResolver);

            // assert
            Assert.That(result.Value, Is.Not.Empty);
        }
    }
}
