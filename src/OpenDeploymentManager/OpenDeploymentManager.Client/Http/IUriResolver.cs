using System;

namespace OpenDeploymentManager.Client.Http
{
    public interface IUriResolver
    {
        Uri BaseUri { get; }

        Uri RootUri { get; }

        Uri Resolve(string uri);
    }
}