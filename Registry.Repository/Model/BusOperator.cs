using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Registry.Repository.Model;

public class BusOperator
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [IgnoreDataMember]
    public string? Id { get; init; }

    public string BusName { get; set; } = string.Empty;
    public string OpImg { get; set; } = string.Empty;
    public int Status { get; set; }
    public int Rate { get; set; }
    public float AgentCommission { get; set; }
    public float AdminCommission { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Lats { get; set; } = string.Empty;
    public string Longs { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string IfscCode { get; set; } = string.Empty;
    public string ReceiptName { get; set; } = string.Empty;
    public string AccNo { get; set; } = string.Empty;
    public string PayId { get; set; } = string.Empty;
    public string UpiId { get; set; } = string.Empty;
    public int DarkMode { get; set; } = 0; // Default value
}