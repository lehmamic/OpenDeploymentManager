using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeploymentManager.Deployment
{
    public interface IDeploymentExtensionsFactory
    {
        T CreateExtension<T>();
    }
}
