using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace OpenDeploymentManager.Agent.Contracts
{
    [DataContract]
    public class KeyValue<TValue>
    {
        [Required]
        [DataMember]
        public string Key { get; set; }

        [Required]
        [DataMember]
        public TValue Value { get; set; }
    }
}