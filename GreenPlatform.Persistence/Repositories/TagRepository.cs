using Domain.Repositories;

namespace Persistence.Repositories;

public class TagRepository : BaseRepository, ITagRepository
{
    public TagRepository(GreenPlatformDbContext context) : base(context)
    {
    }
}
