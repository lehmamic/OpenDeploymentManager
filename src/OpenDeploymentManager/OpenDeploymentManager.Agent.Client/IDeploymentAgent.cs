using System;

namespace OpenDeploymentManager.Agent.Client
{
    public interface IDeploymentAgent
    {
        Uri Uri { get; }

        string MachineName { get; }

        Version Version { get; }

        T GetService<T>() where T : class;
    }
}