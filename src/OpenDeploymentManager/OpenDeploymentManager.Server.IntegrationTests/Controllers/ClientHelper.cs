using NUnit.Framework;
using OpenDeploymentManager.Client;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Common;
using OpenDeploymentManager.Server.Contracts;
using OpenDeploymentManager.Server.Host;

namespace OpenDeploymentManager.Server.IntegrationTests.Controllers
{
    public static class ClientHelper
    {
        public static IOpenDeploymentManagerClient CreateAdminClient()
        {
            return CreateClient("Admin", "123456");
        }

        public static IOpenDeploymentManagerClient CreateClient(string username, string password)
        {
            return CreateClient(new BearerTokenAuthentication(username, password));
        }

        public static IOpenDeploymentManagerClient CreateClient(string username, string password, bool createUserIfNotExist)
        {
            if (createUserIfNotExist)
            {
                CreateUser(username, password);
            }

            return CreateClient(new BearerTokenAuthentication(username, password));
        }

        public static IOpenDeploymentManagerClient CreateClient(IOpenDeploymentManagerAuthentication authentication)
        {
            var clientFactory = new OpenDeploymentManagerClientFactory();

            var endpoint = new OpenDeploymentManagerEndpoint(
                ServerConfiguration.ServerUrl.ToUri(),
                authentication);

            return clientFactory.CreateClient(endpoint);
        }

        public static User CreateUser(string username, string password)
        {
            var client = CreateAdminClient();

            User user;
            try
            {
                user = client.GetService<IUserRepository>().GetById(username);
            }
            catch (NotFoundException)
            {
                user = client.GetService<IUserRepository>().Create(
                    new CreateUser
                    {
                        UserName = username,
                        Password = password,
                        ConfirmPassword = password
                    });
            }

            return user;
        }

        public static void AssertCanLogin(string username, string password)
        {
            var authentication = new BearerTokenAuthentication(username, password);
            var endpoint = new OpenDeploymentManagerEndpoint(ServerConfiguration.ServerUrl.ToUri(), authentication);
            AuthenticationHeaderValue result = authentication.Authenticate(endpoint.UriResolver);

            Assert.That(result, Is.Not.Null);
        }
    }
}
