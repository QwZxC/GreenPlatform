using Domain.Dtos;
using GreenPlatform.Domain.Dtos;

namespace Domain.Services;

public interface IGeocodingService
{
    Task<GeocordingDto> GetCoordinatesAsync(GetAirPolutionRequest request);
}