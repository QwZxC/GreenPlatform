using Common.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация ICommentRepository
/// Так же реализует в себе IBaseRepository,
/// путём наследования от BaseRepository
/// </summary>
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
            .OrderBy(comment => comment.CreationDate)
            .Where(comment => comment.ArticleId == articleId)
            .ToListAsync();
    }

    /// <summary>
    /// В случае не нахождения подходящего элемента выкидывает исключение
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
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
