using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host.Models.Entity;
using OpenDeploymentManager.Server.Host.Security;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    [TestFixture]
    public class UserGroupsControllerFixture
    {
        [Test]
        public void Query_WithTop_ReturnsItems()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            // act
            var query = new PagingQuery<UserGroup> { Top = 1, Skip = 0 };
            PagingResult<UserGroup> result = target.Query(query);

            // assert
            Assert.That(result.Items.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetById_WithExistingId_ReturnsUserGroup()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            // act
            var actual = target.GetById(WellKnownEntityKeys.AdministratorsUserGroup);

            // assert
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        public void Create_WithValidUserGroup_CreatesUserGroup()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            // act
            var userGroup = new UserGroup { Name = "CreateUserGroupTest1" };
            var created = target.Create(userGroup);

            // assert
            var actual = target.GetById(created.Id);
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void Create_WithEmptyName_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            // act
            var userGroup = new UserGroup();
            var created = target.Create(userGroup);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void Create_WithDuplicateName_ThrowsException()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            target.Create(new UserGroup { Name = "CreateUserGroupTest2" });

            // act
            var created = target.Create(new UserGroup { Name = "CreateUserGroupTest2" });
        }

        [Test]
        public void Update_WithValidUserGroup_UpdatesUserGroup()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            var userGroup = target.Create(new UserGroup { Name = "UpdateUserGroupTest1" });

            // act
            userGroup.Name = "Update2UserGroup";
            target.Update(userGroup.Id, userGroup);

            // assert
            var actual = target.GetById(userGroup.Id);
            Assert.That(actual.Name, Is.EqualTo(userGroup.Name));
        }

        [Test]
        public void Delete_WithExistingUserGroup_DeletesUserGroup()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            var userGroup = target.Create(new UserGroup { Name = "DeleteUserGroupTest1" });

            // act
            target.Delete(userGroup.Id);

            // assert
            Assert.Throws<NotFoundException>(() => target.GetById(userGroup.Id));
        }

        [Test]
        public void GetResources_ReturnsResources()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            // act
            IEnumerable<string> resources = target.GetResources();

            // arrange
            CollectionAssert.IsNotEmpty(resources);
        }

        [Test]
        public void GetResoureOperations_WithValidResoureName_ReturnsResources()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IUserGroupRepository>();

            // act
            IEnumerable<string> operations = target.GetResourceOperations(GlobalResources.User.ToResourceName());

            // arrange
            CollectionAssert.IsNotEmpty(operations);
        }
    }
}
