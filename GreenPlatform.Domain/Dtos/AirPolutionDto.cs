using System.Text.Json.Serialization;

namespace Domain.Dtos;

public record AirPolutionDto
{
    [JsonPropertyName("coord")]
    public Dictionary<string, double> Coord { get; set; }
    [JsonPropertyName("list")]
    public List<AirPolutionInfoDto> List { get; set; }
    [JsonPropertyName("dt")]
    public long Dt { get; set; }
}