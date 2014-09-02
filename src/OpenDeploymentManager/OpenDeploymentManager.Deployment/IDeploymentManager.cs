using System.Collections.Generic;

namespace OpenDeploymentManager.Deployment
{
    public interface IDeploymentManager
    {
        void RunDeployment(string template, IDictionary<string, object> arguments, IDictionary<string, string> variables);
    }
}