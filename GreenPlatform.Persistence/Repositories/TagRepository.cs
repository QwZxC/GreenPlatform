using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(GreenPlatformDbContext context) : base(context)
    {
    }
}
