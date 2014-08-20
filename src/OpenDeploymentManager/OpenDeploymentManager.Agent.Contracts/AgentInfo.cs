using System.Runtime.Serialization;

namespace OpenDeploymentManager.Agent.Contracts
{
    [DataContract]
    public class AgentInfo
    {
        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public string Version { get; set; }
    }
}