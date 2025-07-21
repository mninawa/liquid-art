using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registry.Repository.Model;

public class City
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [IgnoreDataMember]
    public string? Id { get; init; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("status")]
    public int Status { get; set; }
}