using System;
using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    [TestFixture]
    public class UsersControllerFixture
    {
        [Test]
        public void GetUserById_WithExistingUserId_ReturnsUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var actual = target.GetUserById("Admin");

            // assert
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        public void CreateUser_WithValidUser_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest", Password = "123456", ConfirmPassword = "123456" };
            target.CreateUser(user);

            // assert
            var created = target.GetUserById(user.UserName);
            Assert.That(created, Is.Not.Null);
        }

        private static IOpenDeploymentManagerClient CreateClient(IOpenDeploymentManagerAuthentication authentication)
        {
            var clientFactory = new OpenDeploymentManagerClientFactory();

            var endpoint = new OpenDeploymentManagerEndpoint(
                new Uri(ServerConfiguration.ServerUrl),
                authentication);

            return clientFactory.CreateClient(endpoint);
        }
    }
}
