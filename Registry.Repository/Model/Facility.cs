using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registry.Repository.Model;

public class Facility
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [IgnoreDataMember]
    public string? Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Img { get; set; } = string.Empty;
    public int Status { get; set; }
}