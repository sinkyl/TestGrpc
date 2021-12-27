using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace TestGrpc.Models
{
    [BsonDiscriminator("Base", RootClass = true)]
    [DataContract]
    public abstract class Base
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [ProtoMember(1)]
        public string Id { get; set; }
        
        [BsonDateTimeOptions]
        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

        [BsonDateTimeOptions]
        [ProtoMember(3, DataFormat = DataFormat.WellKnown)]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}