using Domain.Dtos;
using Domain.Services;
using GreenPlatform.Domain.Dtos;
using Newtonsoft.Json;

namespace GreenPlatform.Core;

public class GeocodingService : IGeocodingService
{
    private readonly HttpClient _httpClient;

    public GeocodingService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<GeocordingDto> GetCoordinatesAsync(GetAirPolutionRequest request)
    {
        var response = await _httpClient.GetAsync($"http://api.openweathermap.org/geo/1.0/direct?q={request.City}&appid=736972c49a11443dc0d6a94111fee2d2");
        string body = await response.Content.ReadAsStringAsync();
        var list = JsonConvert.DeserializeObject<List<GeocordingDto>>(body);
        return list.FirstOrDefault();
    }
}
