using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notifications.Repository.Model;

public class Template
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Content { get; set; } 
    public string Version { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}