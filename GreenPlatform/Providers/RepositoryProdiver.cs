using Domain.Repositories;
using Infrastructure.Repositories;

namespace GreenPlatform.Providers;

public static class RepositoryProdiver
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ISubscribeRepository, SubscribeRepository>();
    }
}
