using System.Text.Json.Serialization;

namespace Domain.Dtos;

public record AirPolutionInfoDto
{
    [JsonPropertyName("aqi")]
    public int Aqi { get; set; }
    [JsonPropertyName("components")]
    public Dictionary<string, double> Components { get; set; }
}
