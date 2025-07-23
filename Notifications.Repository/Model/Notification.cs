using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notifications.Repository.Model
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string messageId { get; set; }
        public string? ClientId { get; set; }
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string? Mobile { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "Unread";
        public DateTime EventDate { get; set; } 
    }
}
