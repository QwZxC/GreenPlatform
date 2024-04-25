using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class GreenPlatformDbContext : DbContext
{

    public GreenPlatformDbContext(DbContextOptions<GreenPlatformDbContext> options)
        : base(options)
    {

    }
    
    public DbSet<Role> Role { get; set; }
    public DbSet<GreenPlatformUser> GreenPlatformUser { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Tag> Tag { get; set; }
}
