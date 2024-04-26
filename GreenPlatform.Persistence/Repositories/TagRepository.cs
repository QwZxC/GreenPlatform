using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(GreenPlatformDbContext context) : base(context)
    {
    }
}
