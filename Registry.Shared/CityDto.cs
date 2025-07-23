using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Registry.Shared;

public class CityDto
{

    [JsonProperty("id")]
    public string? Id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;
    [JsonProperty("status")]
    public bool Status { get; set; }
}