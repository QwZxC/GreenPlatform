using System.Security.Claims;
using Domain.Entities;

namespace Service.Abstractions;

public interface ITokenService
{
    /// <summary>
    /// Генерирует Refresh-токен для пользователя.
    /// </summary>
    /// <returns></returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Создаёт access-токен.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    string GenerateAccessToken(GreenPlatformUser user, List<Role> roles);
}
