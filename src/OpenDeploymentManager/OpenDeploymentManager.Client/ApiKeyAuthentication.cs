using System;

namespace OpenDeploymentManager.Client
{
    public class ApiKeyAuthentication : IOpenDeploymentManagerAuthentication
    {
        private readonly string apiKey;

        public ApiKeyAuthentication(string apiKey)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException("apiKey");
            }

            this.apiKey = apiKey;
        }

        #region Implementation of IOpenDeploymentManagerAuthentication
        public AuthenticationHeaderValue Authenticate(IUriResolver uriResolver)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}