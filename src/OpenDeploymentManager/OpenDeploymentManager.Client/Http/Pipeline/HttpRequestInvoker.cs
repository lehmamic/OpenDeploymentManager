using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using Castle.DynamicProxy;
using OpenDeploymentManager.Client.Http.Middleware;
using OpenDeploymentManager.Server.Contracts.Http;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal class HttpRequestInvoker : IHttpRequestBuilder, IMiddleware, IDisposable
    {
        private readonly HttpClient client = HttpClientFactory.Create();
        private readonly List<IMiddleware> middlewarePipeline = new List<IMiddleware>();

        private bool disposed;

        public HttpRequestInvoker(Uri baseAddress)
        {
            this.client.BaseAddress = baseAddress;
        }

        ~HttpRequestInvoker()
        {
            this.Dispose(false);
        }

        public void Configure(Action<IHttpRequestBuilder> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder(this);
        }

        public IHttpResponseContext Execute(IHttpRequestContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var pipeline = new MiddlewarePipeline(this.middlewarePipeline);
            return pipeline.Invoke(context, this);
        }

        #region Implementation of IRequestBuilder
        IHttpRequestBuilder IHttpRequestBuilder.Use(IMiddleware middleware)
        {
            if (middleware == null)
            {
                throw new ArgumentNullException("middleware");
            }

            this.middlewarePipeline.Add(middleware);
            return this;
        }
        #endregion

        #region Implementation of IMiddleware
        IHttpResponseContext IMiddleware.Invoke(IHttpRequestContext context, INextInvoker next)
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

        #region Implementation of IDisposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.client.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}

