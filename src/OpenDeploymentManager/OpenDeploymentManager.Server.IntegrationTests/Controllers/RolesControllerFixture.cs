using System.Linq;
using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Server.Contracts;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    [TestFixture]
    public class RolesControllerFixture
    {
        [Test]
        public void Query_WithTop_ReturnsItems()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IRoleRepository>();

            // act
            var query = new PagingQuery<Role> { Top = 1, Skip = 0 };
            PagingResult<Role> result = target.Query(query);

            // assert
            Assert.That(result.Items.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetRoleById_WithExistingRoleId_ReturnsRole()
        {
            // arrange
            IOpenDeploymentManagerClient client = ClientHelper.CreateAdminClient();
            var target = client.GetService<IRoleRepository>();

            // act
            var actual = target.GetById("Administrator");

            // assert
            Assert.That(actual, Is.Not.Null);
        }
    }
}
