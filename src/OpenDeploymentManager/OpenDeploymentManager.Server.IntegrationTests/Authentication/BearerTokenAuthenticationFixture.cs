
using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Client.Exceptions;
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

        [Test]
        [ExpectedException(typeof(SecurityException))]
        public void Authenticate_WithInvalidCredentials_ThrowsException()
        {
            // arrange
            var target = new BearerTokenAuthentication("admin", "wrongpassword");
            var endpoint = new OpenDeploymentManagerEndpoint(ServerConfiguration.ServerUrl.ToUri(), target);

            // act
            target.Authenticate(endpoint.UriResolver);
        }

        [Test]
        [ExpectedException(typeof(CommunicationException))]
        public void Authenticate_WithInvalidUrl_ThrowsException()
        {
            // arrange
            var target = new BearerTokenAuthentication("admin", "123456");
            var endpoint = new OpenDeploymentManagerEndpoint("http://localhost:12345".ToUri(), target);

            // act
            target.Authenticate(endpoint.UriResolver);
        }
    }
}
