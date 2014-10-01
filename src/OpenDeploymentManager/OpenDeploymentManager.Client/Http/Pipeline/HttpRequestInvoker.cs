﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using OpenDeploymentManager.Client.Http.Middleware;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal class HttpRequestInvoker : IHttpRequestBuilder, IDisposable
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
            return pipeline.Invoke(context, new SendRequestMiddleware(this.client));
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
