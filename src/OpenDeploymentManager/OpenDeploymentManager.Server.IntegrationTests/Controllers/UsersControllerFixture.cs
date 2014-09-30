using System;
using System.Linq;
using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    [TestFixture]
    public class UsersControllerFixture
    {
        [Test]
        public void Query_WithEmptyQuery_ReturnsItems()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var query = new PagingQuery<User> { Top = 1, Skip = 0 };
            PagingResult<User> result = target.Query(query);

            // assert
            Assert.That(result.Items.Count(), Is.EqualTo(1));
        }

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

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithoutUserName_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { Password = "123456", ConfirmPassword = "123456" };
            target.CreateUser(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithWrongPasswordConfirmation_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest", Password = "123456", ConfirmPassword = "1234567" };
            target.CreateUser(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithPasswordToShort_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest", Password = "1234", ConfirmPassword = "1234" };
            target.CreateUser(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithoutPasswort_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest" };
            target.CreateUser(user);
        }

        [Test]
        public void UpdateUser_WithValidUser_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            User user = target.CreateUser(new CreateUser { UserName = "UpdateUserTest1", Password = "123456", ConfirmPassword = "123456" });

            // act
            user.DisplayName = "TestDisplayName";
            target.UpdateUser(user.UserName, user);


            // assert
            var updated = target.GetUserById(user.UserName);
            Assert.That(updated.DisplayName, Is.EqualTo(user.DisplayName));
        }

        [Test]
        [ExpectedException(typeof(ServerException))]
        public void UpdateUser_WithUserDoesNotExist_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = CreateClient(new BearerTokenAuthentication("Admin", "123456"));
            var target = client.GetService<IUserRepository>();

            User user = target.CreateUser(new CreateUser { UserName = "UpdateUserTest2", Password = "123456", ConfirmPassword = "123456" });

            // act
            user.UserName = "TestDisplayName1";
            target.UpdateUser(user.UserName, user);
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
