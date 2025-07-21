using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Registry.Shared;

public class CityDto
{
    [IgnoreDataMember]
    [JsonProperty("id")]
    public string? Id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    [JsonProperty("status")]
    public int Status { get; set; }
}