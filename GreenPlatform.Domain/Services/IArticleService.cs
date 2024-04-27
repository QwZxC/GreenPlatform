using Domain.Entities;

namespace Domain.Services;

public interface IArticleService
{
    Task<List<Article>> FindAllArticlesAsync();
    Task<Article> FindArticleByIdAsync(Guid id);
}
