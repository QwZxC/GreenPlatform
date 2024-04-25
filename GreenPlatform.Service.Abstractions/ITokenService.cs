using System.Security.Claims;
using Domain.Entities;

namespace Service.Abstractions;

public interface ITokenService
{
    ClaimsPrincipal GetPrincipal(string accessToken);

    /// <summary>
    /// Генерирует Refresh-токен для пользователя.
    /// </summary>
    /// <returns></returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Возвращает время истечения Refresh-токена.
    /// </summary>
    /// <returns></returns>
    DateTime GetRefreshTokenExpiryTime();

    /// <summary>
    /// Делает Refresh-токены всех пользователей null.
    /// </summary>
    /// <returns></returns>
    Task RevokeAllAsync();

    /// <summary>
    /// Создаёт access-токен.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    string CreateToken(GreenPlatformUser user, List<Role> roles);
}
