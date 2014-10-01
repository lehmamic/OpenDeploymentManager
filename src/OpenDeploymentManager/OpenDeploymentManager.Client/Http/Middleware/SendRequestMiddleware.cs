using System;
using System.Globalization;
using System.Net.Http;
using OpenDeploymentManager.Client.Http.Pipeline;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client.Http.Middleware
{
    internal class SendRequestMiddleware : IMiddleware
    {
        private readonly HttpClient client;

        public SendRequestMiddleware(HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            this.client = client;
        }

        #region Implementation of IMiddleware
        public IHttpResponseContext Invoke(IHttpRequestContext context, INextInvoker next)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.Request.RequestUri = string.Format(
                CultureInfo.InvariantCulture,
                "{0}{1}",
                context.Request.RequestUri.ToString().TrimEnd('/'),
                context.Request.GetUriQuery()).ToUri(UriKind.Relative);

            HttpResponseMessage response = this.client.SendAsync(context.Request).WaitOn();
            return new HttpResponseContext(response);
        }
        #endregion
    }
}
