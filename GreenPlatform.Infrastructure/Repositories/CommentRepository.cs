using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(GreenPlatformDbContext context) : base(context)
    {
    }
}
