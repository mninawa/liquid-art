using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Registry.Repository.Model;

public class City
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonProperty("id")]
    public string? Id { get; init; }

    [BsonElement("title")]
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("status")]
    [JsonProperty("status")]
    public bool Status { get; set; }
}