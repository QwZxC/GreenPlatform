using Domain.Dtos;

namespace Domain.Services;

public interface IEcologyService
{
    /// <summary>
    /// Возвращает информацию о загрязнении воздуха
    /// </summary>
    /// <returns></returns>
    Task<AirPolutionDto> GetAirPolutionInfoAsync(GetAirPolutionRequest request);
}