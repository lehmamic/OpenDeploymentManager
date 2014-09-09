using System;

namespace OpenDeploymentManager.Client
{
    public class OpenDeploymentManagerEndpoint
    {
        private readonly IUriResolver uriResolver;
        private readonly IOpenDeploymentManagerAuthentication authentication;

        public OpenDeploymentManagerEndpoint(Uri uri, IOpenDeploymentManagerAuthentication authentication)
            : this(new UriResolver(uri), authentication)
        {
        }

        private OpenDeploymentManagerEndpoint(IUriResolver uriResolver, IOpenDeploymentManagerAuthentication authentication)
        {
            if (uriResolver == null)
            {
                throw new ArgumentNullException("uriResolver");
            }

            if (authentication == null)
            {
                throw new ArgumentNullException("authentication");
            }

            this.uriResolver = uriResolver;
            this.authentication = authentication;
        }

        public IUriResolver UriResolver
        {
            get
            {
                return this.uriResolver;
            }
        }

        public IOpenDeploymentManagerAuthentication Authentication
        {
            get
            {
                return this.authentication;
            }
        }
    }
}