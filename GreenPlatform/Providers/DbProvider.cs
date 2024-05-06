using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GreenPlatform.Providers;

public static class DbProvider
{
    public static async Task AddDatabaseAsync(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<GreenPlatformDbContext>(optinons =>
            optinons.UseNpgsql(connectionString));
        await MigrateAsync(connectionString);
    }

    private static async Task MigrateAsync(string connectionString)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<GreenPlatformDbContext>()
            .UseNpgsql(connectionString);
        using (var dbContext = new GreenPlatformDbContext(optionsBuilder.Options))
        {
            await dbContext.Database.MigrateAsync();
            if (!dbContext.Role.Any())
            {
                dbContext.Role.Add(new Domain.Entities.Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "User"
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
