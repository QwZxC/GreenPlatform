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

    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string CreateToken(GreenPlatformUser user, List<Role> roles)
    {
        var token = user
                .CreateClaims(roles)
                .CreateJwtToken(configuration);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetPrincipal(string accessToken)
    {
        throw new NotImplementedException();
    }

    public DateTime GetRefreshTokenExpiryTime()
    {
        throw new NotImplementedException();
    }

    public Task RevokeAllAsync()
    {
        throw new NotImplementedException();
    }
}
