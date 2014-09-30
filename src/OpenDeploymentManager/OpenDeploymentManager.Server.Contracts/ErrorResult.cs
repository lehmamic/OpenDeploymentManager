using System;
using System.Collections.Generic;

namespace OpenDeploymentManager.Server.Contracts
{
    [Serializable]
    public class ErrorResult
    {
        public ErrorResult()
        {
            this.ModelState = new Dictionary<string, string[]>();

        }

        public string Message { get; set; }

        public Dictionary<string, string[]> ModelState { get; set; }
    }
}
