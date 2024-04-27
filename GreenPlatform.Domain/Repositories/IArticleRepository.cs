using Domain.Entities;
using Domain.Repositories;

namespace Domain.Repositories;

public interface IArticleRepository : IBaseRepository<Article>
{
    Task<Article> FindArticleByIdAsync(Guid id);
}
