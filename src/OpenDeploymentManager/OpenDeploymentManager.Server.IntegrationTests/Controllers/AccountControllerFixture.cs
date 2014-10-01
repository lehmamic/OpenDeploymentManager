using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Server.Contracts;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    [TestFixture]
    public class AccountControllerFixture
    {
        [Test]
        public void Get_WithAdminUser_ReturnsUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IAccountRepository>();

            // act
            User user = target.Get();

            // assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.UserName, Is.EqualTo("Admin"));
        }

        [Test]
        public void Update_WithValidUser_ReturnsUser()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateClient("AccountUpdateUserTest", "123456", createUserIfNotExist: true);
            var target = client.GetService<IAccountRepository>();

            User user = target.Get();

            // act
            user.DisplayName = "MyUser";
            target.Update(user);

            // assert
            Assert.That(target.Get().DisplayName, Is.EqualTo(user.DisplayName));
        }

        [Test]
        public void ChangePassword_WithValidPassword_CanUseNewPassword()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateClient("AccountChangePasswordUserTest1", "123456", createUserIfNotExist: true);
            var target = client.GetService<IAccountRepository>();

            // act
            var password = new ChangePassword
                               {
                                   OldPassword = "123456",
                                   NewPassword = "asdfgh",
                                   ConfirmPassword = "asdfgh",
                               };

            target.ChangePassword(password);

            // assert
            ClientHelper.AssertCanLogin("AccountChangePasswordUserTest1", password.NewPassword);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ChangePassword_WithOldPasswordIsWrong_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateClient("AccountChangePasswordUserTest2", "123456", createUserIfNotExist: true);
            var target = client.GetService<IAccountRepository>();

            // act
            var password = new ChangePassword
            {
                OldPassword = "1234567",
                NewPassword = "asdfgh",
                ConfirmPassword = "asdfgh",
            };

            target.ChangePassword(password);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void ChangePassword_WithPasswordConfirmationIsWrong_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateClient("AccountChangePasswordUserTest3", "123456", createUserIfNotExist: true);
            var target = client.GetService<IAccountRepository>();

            // act
            var password = new ChangePassword
            {
                OldPassword = "123456",
                NewPassword = "asdfgha",
                ConfirmPassword = "asdfgh",
            };

            target.ChangePassword(password);
        }

        ////[Test]
        ////public void Logout_WithValidUser_CanNotUseClientAnymore()
        ////{
        ////    // arrange
        ////    IOpenDeploymentManagerClient client = ClientHelper.CreateClient("AccountUpdateUserTest", "123456", createUserIfNotExist: true);
        ////    var target = client.GetService<IAccountRepository>();

        ////    // act
        ////    target.LogOff();

        ////    // assert
        ////    Assert.Throws<SecurityException>(() => target.Get());
        ////}
    }
}
