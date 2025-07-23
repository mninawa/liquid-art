using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registry.Repository.Model;

public class Facility
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    [BsonRequired]
    [BsonElement("title")]
    public string Title { get; set; }

    [BsonRequired]
    [BsonElement("img")]
    public string Img { get; set; }

    [BsonRequired]
    [BsonElement("status")]
    public int Status { get; set; }
}