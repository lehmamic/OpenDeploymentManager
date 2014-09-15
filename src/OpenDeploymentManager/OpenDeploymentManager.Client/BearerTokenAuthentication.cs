using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using OpenDeploymentManager.Client.Exceptions;
using OpenDeploymentManager.Client.Formatting;
using OpenDeploymentManager.Client.Http;
using OpenDeploymentManager.Client.Properties;
using OpenDeploymentManager.Server.Contracts;

namespace OpenDeploymentManager.Client
{
    public class BearerTokenAuthentication : IOpenDeploymentManagerAuthentication
    {
        private readonly NetworkCredential credentials;

        private string token;

        public BearerTokenAuthentication(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("The token is null or empty.", "token");
            }

            this.token = token;
        }

        public BearerTokenAuthentication(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The username is null or empty.", "username");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The password is null or empty.", "password");
            }

            this.credentials = new NetworkCredential(username, password);
        }

        public ICredentials Credentials
        {
            get
            {
                return this.credentials;
            }
        }

        public bool HasToken
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.token);
            }
        }

        public string Token
        {
            get
            {
                return this.token;
            }
        }

        #region Implementation of IOpenDeploymentManagerAuthentication
        public AuthenticationHeaderValue Authenticate(IUriResolver uriResolver)
        {
            if (!this.HasToken)
            {
                this.RequestToken(uriResolver);
            }

            return new AuthenticationHeaderValue(
                "Authorization",
                string.Format(CultureInfo.InvariantCulture, "Bearer {0}", this.Token));
        }

        private void RequestToken(IUriResolver uriResolver)
        {
            using (HttpClient client = HttpClientFactory.Create())
            {
                client.BaseAddress = uriResolver.RootUri;

                try
                {
                    HttpRequestMessage request = this.CreateHttpRequestMessage(uriResolver);
                    HttpResponseMessage response = client.SendAsync(request).WaitOn();

                    this.ProcessResponse(response);
                }
                catch (HttpRequestException e)
                {
                    throw new CommunicationException(e.Message, e);
                }
            }
        }

        private HttpRequestMessage CreateHttpRequestMessage(IUriResolver uriResolver)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "Token");

            NetworkCredential networkCredential = this.Credentials.GetCredential(uriResolver.Resolve("~/Token"), "Bearer");
            var requestData = new TokenRequest
                                  {
                                      grant_type = "password",
                                      username = networkCredential.UserName,
                                      password = networkCredential.Password
                                  };

            request.Content = new ObjectContent(
                typeof(TokenRequest),
                requestData,
                new WritableFormUrlEncodedMediaTypeFormatter());
            return request;
        }

        private void ProcessResponse(HttpResponseMessage response)
        {
            var responseData = response.Content.ReadAsAsync<TokenResponse>().WaitOn();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    this.token = responseData.access_token;
                    break;

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.BadRequest:
                    throw new SecurityException(responseData.error_description, (int)response.StatusCode);

                default:
                    string message = responseData.error_description ?? Resources.OpenDeploymentManagerClient_UnknownError;
                    throw new ServerException(message, (int)response.StatusCode);
            }
        }
        #endregion
    }
}