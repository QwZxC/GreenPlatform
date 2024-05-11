using Domain;
using Domain.Dtos;
using Domain.Services;
using GreenPlatform.Domain.Dtos;
using Newtonsoft.Json;

namespace Core;

public class EcologyService : IEcologyService
{
    private readonly HttpClient _httpClient;
    private readonly IGeocodingService _geocodingService;

    public EcologyService(IHttpClientFactory httpClientFactory, IGeocodingService geocodingService)
    {
        _httpClient = httpClientFactory.CreateClient();
        _geocodingService = geocodingService;
    }

    public async Task<AirPolutionDto> GetAirPolutionInfoAsync(GetAirPolutionRequest request)
    {
        GeocordingDto geocordingDto = await _geocodingService.GetCoordinatesAsync(request);
        if (geocordingDto == null)
        {
            return null;
        }
        var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/air_pollution?lat={geocordingDto.Lat}&lon={geocordingDto.Lon}&appid=736972c49a11443dc0d6a94111fee2d2");
        string body = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AirPolutionDto>(body);
    }
}
