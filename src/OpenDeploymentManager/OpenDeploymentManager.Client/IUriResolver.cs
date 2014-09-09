using System;

namespace OpenDeploymentManager.Client
{
    public interface IUriResolver
    {
        Uri BaseUri { get; }

        Uri RootUri { get; }

        Uri Resolve(string uri);
    }
}