using System;

namespace OpenDeploymentManager.Client
{
    internal class UriResolver : IUriResolver
    {
        private readonly Uri baseUri;
        private readonly Uri rootUri;

        public UriResolver(Uri uri)
            : this(uri, "/api")
        {
        }

        public UriResolver(Uri uri, string allUrisStartWith)
        {
            allUrisStartWith = allUrisStartWith.AppendIfNotEndsWith("/");

            this.baseUri = uri.ExtractBaseUri(allUrisStartWith);
            this.rootUri = this.baseUri.ExtractRootUri();
        }

        public Uri BaseUri
        {
            get
            {
                return this.baseUri;
            }
        }

        public Uri RootUri
        {
            get
            {
                return this.rootUri;
            }
        }

        public Uri Resolve(string uri)
        {
            if (uri.StartsWith("~/"))
            {
                return new Uri(this.BaseUri + "/" + uri.TrimStart('~', '/'));
            }
            else
            {
                if (!uri.StartsWith("/"))
                {
                    uri = (string)(object)'/' + (object)uri;
                }

                return new Uri(this.RootUri + uri);
            }
        }

        public override string ToString()
        {
            return this.BaseUri.ToString();
        }
    }
}