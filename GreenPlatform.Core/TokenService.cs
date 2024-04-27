using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Domain.Services;
using Common.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace Core;

public class TokenService : ITokenService
{
    private readonly IConfiguration configuration;

    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
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
}
