using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notifications.Repository.Model
{
    public class OutboxMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Payload { get; set; }
        public string? Topic { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Processed { get; set; }
    }
}
