using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Common.Extensions;

public static class JwtBearerExtensions
{
    public static List<Claim> CreateClaims(this GreenPlatformUser user, List<Role> roles)
    {
        var claims = new List<Claim>
            {
                new("UserId", user.Id.ToString()),
                new("Login", user.Login!),
                new("Roles", string.Join(" ", roles.Select(x => x.Name!))),
            };
        return claims;
    }

    public static SigningCredentials CreateSigningCredentials(this IConfiguration configuration)
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JwtAuth:Key"]!)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }

    public static JwtSecurityToken CreateJwtToken(this IEnumerable<Claim> claims, IConfiguration configuration)
    {
        long expire = configuration.GetSection("JwtAuth:Expire").Get<int>();

        return new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expire),
            signingCredentials: configuration.CreateSigningCredentials()
        );
    }

    public static JwtSecurityToken CreateToken(this IConfiguration configuration, List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtAuth:Key"]!));
        var tokenValidityInMinutes = configuration.GetSection("JwtAuth:TokenValidityInMinutes").Get<long>();

        var token = new JwtSecurityToken(
            issuer: configuration["JwtAuth:Issuer"],
            audience: configuration["JwtAuth:Audience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    public static string GenerateRefreshToken(this IConfiguration configuration)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static ClaimsPrincipal? GetPrincipalFromExpiredToken(this IConfiguration configuration, string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtAuth:Secret"]!)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
