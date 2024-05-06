using Common.Exceptions;
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

    public async Task<Comment> FindLastUserCommentForArticleAsync(Guid articleId, Guid userId)
    {
        return await _context
            .Comment.Include(comment => comment.Creator)
            .OrderByDescending(comment => comment.CreationDate)
            .FirstOrDefaultAsync(comment =>
            comment.ArticleId == articleId && comment.CreatorId == userId)
            ?? throw new NotFoundException("Не удалось найти комментарий");
    }
}
