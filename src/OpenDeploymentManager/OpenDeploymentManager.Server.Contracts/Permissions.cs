using System.Collections.Generic;

namespace OpenDeploymentManager.Server.Contracts
{
    public class Permissions
    {
        public Permissions()
        {
            this.AllowedOperations = new List<string>();
            this.DeniedOperations = new List<string>();
        }

        public List<string> AllowedOperations { get; set; }

        public List<string> DeniedOperations { get; set; }
    }
}