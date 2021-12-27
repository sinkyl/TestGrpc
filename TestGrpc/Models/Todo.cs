using ProtoBuf;

namespace TestGrpc.Models
{
    [ProtoContract]
    public class Todo : Base
    {
        [ProtoMember(1)]
        public string Title { get; set; }
        [ProtoMember(2)]
        public string Content { get; set; }
        [ProtoMember(3)]
        public string Category { get; set; }
    }
}