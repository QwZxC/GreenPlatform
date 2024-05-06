using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация ITagRepository
/// Так же реализует в себе IBaseRepository,
/// путём наследования от BaseRepository
/// </summary>
public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(GreenPlatformDbContext context) : base(context)
    {
    }
}
