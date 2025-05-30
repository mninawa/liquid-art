using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registry.Repository.Model
{
    public class OutboxMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [IgnoreDataMember] 
        public string? Id { get; init; }
        public string? Payload { get; set; }
        public string? Topic { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Processed { get; set; }
    }
}
