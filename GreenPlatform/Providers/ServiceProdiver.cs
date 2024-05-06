using Core;
using Domain.Services;

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
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}
