using Domain.Entities;
using Domain.Repositories;

namespace Domain.Repositories;

public interface IArticleRepository : IBaseRepository<Article>
{
    Task<List<Article>> FindAllArticlesForUserAsync(Guid userId);
    Task<Article> FindArticleByIdAsync(Guid id);
}
