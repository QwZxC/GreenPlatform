using Domain.Entities;
using Domain.Repositories;

namespace Domain.Repositories;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<List<Comment>> FindCommentsByArticleId(Guid articleId);
}