using System;
using System.Collections.Generic;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public class DeploymentVariablesExtension : IDeploymentVariablesExtension
    {
        private readonly IDictionary<string, object> variablies;

        public DeploymentVariablesExtension(IDictionary<string, object> variablies)
        {
            if (variablies == null)
            {
                throw new ArgumentNullException("variablies");
            }

            this.variablies = variablies;
        }

        #region Implementation of IDeploymentVariablesExtension
        public T Get<T>(string key)
        {
            return (T)this.variablies[key];
        }
        #endregion
    }
}