using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registry.Repository.Model
{
    public class Device
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [IgnoreDataMember] 
        public string? Id { get; init; }
        public string? Name { get; set; }
        public string? Make { get; set; }
        public string? SerialNo { get; set; }
    }
}
