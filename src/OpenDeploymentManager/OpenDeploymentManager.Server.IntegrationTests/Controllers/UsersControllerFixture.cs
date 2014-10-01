using System.Linq;
using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Common;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    [TestFixture]
    public class UsersControllerFixture
    {
        [Test]
        public void Query_WithTop_ReturnsItems()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var query = new PagingQuery<User> { Top = 1, Skip = 0 };
            PagingResult<User> result = target.Query(query);

            // assert
            Assert.That(result.Items.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Query_WithInlineCount_ReturnsTotalCount()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            target.Create(new CreateUser { UserName = "NextPageLinkUserTest1", Password = "123456", ConfirmPassword = "123456" });
            target.Create(new CreateUser { UserName = "NextPageLinkUserTest2", Password = "123456", ConfirmPassword = "123456" });

            // act
            var query = new PagingQuery<User> { Top = 1, Skip = 0, InlineCount = InlineCountOptions.AllPages };
            PagingResult<User> result = target.Query(query);

            // assert
            Assert.That(result.TotalCount, Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void Query_WithSkip_ReturnsItems()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            target.Create(new CreateUser { UserName = "SkipUserTest1", Password = "123456", ConfirmPassword = "123456" });
            target.Create(new CreateUser { UserName = "SkipUserTest2", Password = "123456", ConfirmPassword = "123456" });

            // act
            var query = new PagingQuery<User> { Top = 1, Skip = 1 };
            PagingResult<User> result = target.Query(query);

            // assert
            Assert.That(result.Items.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetUserById_WithExistingUserId_ReturnsUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var actual = target.GetById("Admin");

            // assert
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        public void CreateUser_WithValidUser_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest", Password = "123456", ConfirmPassword = "123456" };
            target.Create(user);

            // assert
            var created = target.GetById(user.UserName);
            Assert.That(created, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithoutUserName_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { Password = "123456", ConfirmPassword = "123456" };
            target.Create(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithWrongPasswordConfirmation_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest", Password = "123456", ConfirmPassword = "1234567" };
            target.Create(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithPasswordToShort_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest", Password = "1234", ConfirmPassword = "1234" };
            target.Create(user);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void CreateUser_WithoutPasswort_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var user = new CreateUser { UserName = "CreateUserTest" };
            target.Create(user);
        }

        [Test]
        public void UpdateUser_WithValidUser_CreatesUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            User user = target.Create(new CreateUser { UserName = "UpdateUserTest1", Password = "123456", ConfirmPassword = "123456" });

            // act
            user.DisplayName = "TestDisplayName";
            target.Update(user.UserName, user);


            // assert
            var updated = target.GetById(user.UserName);
            Assert.That(updated.DisplayName, Is.EqualTo(user.DisplayName));
        }

        [Test]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdateUser_WithUserDoesNotExist_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            User user = target.Create(new CreateUser { UserName = "UpdateUserTest2", Password = "123456", ConfirmPassword = "123456" });

            // act
            user.UserName = "TestDisplayName1";
            target.Update(user.UserName, user);
        }

        [Test]
        public void SetPassword_WithValidPassword_CanUseNewPassword()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            User user = target.Create(new CreateUser { UserName = "SetPasswordUserTest1", Password = "123456", ConfirmPassword = "123456" });

            // act
            var password = new SetPassword
                                     {
                                         NewPassword = "asdfgh",
                                         ConfirmPassword = "asdfgh",
                                     };

            target.SetPassword(user.UserName, password);

            // assert
            ClientHelper.AssertCanLogin(user.UserName, password.NewPassword);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void SetPassword_WithWrongPasswordConfirmation_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            User user = target.Create(new CreateUser { UserName = "SetPasswordUserTest1", Password = "123456", ConfirmPassword = "123456" });

            // act
            var password = new SetPassword
            {
                NewPassword = "asdfgh",
                ConfirmPassword = "asdfgh1",
            };

            target.SetPassword(user.UserName, password);
        }

        [Test]
        [ExpectedException(typeof(NotFoundException))]
        public void SetPassword_WithUserDoesNotExist_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserRepository>();

            // act
            var password = new SetPassword
            {
                NewPassword = "asdfgh",
                ConfirmPassword = "asdfgh1",
            };

            target.SetPassword("AnyNotExistingUser", password);
        }
    }
}
