using System.Text.Json.Serialization;

namespace FantasyCrypto.Core.Dtos;

public class CoinGeckoCoinDto
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonPropertyName("id")] 
    public string CoinGeckoCoinDbId { get; set; } = "";

    [JsonPropertyName("symbol")] 
    public string Symbol { get; set; } = "";

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "";
}
