using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(GreenPlatformDbContext context) : base(context)
    {
    }

    public async Task<List<Comment>> FindCommentsByArticleId(Guid articleId)
    {
        return await _context
            .Comment
            .Include(comment => comment.Creator)
            .Where(comment => comment.ArticleId == articleId)
            .ToListAsync();
    }
}
