using Core;
using Domain.Services;
using GreenPlatform.Core;

namespace GreenPlatform.Providers;

public static class ServiceProdiver
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IEcologyService, EcologyService>();
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}
