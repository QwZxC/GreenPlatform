using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Service.Abstractions;
using Domain.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services;

public class TokenService : ITokenService
{
    
    private readonly IConfiguration configuration;
    private readonly IUserService _userService;

    public TokenService(IConfiguration configuration, IUserService userService)
    {
        this.configuration = configuration;
        _userService = userService;
    }

    public string GenerateAccessToken(GreenPlatformUser user, List<Role> roles)
    {
        var token = user
                .CreateClaims(roles)
                .CreateJwtToken(configuration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return configuration.GenerateRefreshToken();
    }

    public ClaimsPrincipal GetPrincipal(string accessToken)
    {
        throw new NotImplementedException();
    }

    public DateTime GetRefreshTokenExpiryTime()
    {
        return DateTime.UtcNow.AddDays(configuration.GetSection("JwtAuth:RefreshTokenValidityInDays").Get<int>());
    }

    public async Task RevokeAllAsync()
    {
        List<GreenPlatformUser> users = await _userService.FindAllAsync();
        foreach (GreenPlatformUser user in users)
        {
            user.AccessToken = null;
        }
        await _userService.SaveAsync();
    }
}
