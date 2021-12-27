using System.Runtime.Serialization;

namespace TestGrpc.Models
{
    [DataContract]
    public class Query
    {
        [DataMember(Order = 1)]
        public string Filter { get; set; }
    }
}