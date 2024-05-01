using Domain.Entities;

namespace Domain.Repositories;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> FindCommentsByArticleId(Guid articleId);
}