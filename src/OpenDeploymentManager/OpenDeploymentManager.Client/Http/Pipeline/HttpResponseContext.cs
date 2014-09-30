using System;
using System.Net.Http;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal class HttpResponseContext : IHttpResponseContext
    {
        private readonly HttpResponseMessage response;

        public HttpResponseContext(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            this.response = response;
        }

        #region Implementation of IHttpRequestReturn
        public HttpResponseMessage Response
        {
            get
            {
                return this.response;
            }
        }

        public object ReturnValue { get; set; }
        #endregion
    }
}
