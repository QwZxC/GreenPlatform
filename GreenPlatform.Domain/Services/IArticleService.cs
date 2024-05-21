using Domain.Dtos;
using Domain.Entities;

namespace Domain.Services;

public interface IArticleService
{
    Task CreateAsync(CreateArticleViewModel viewModel);
    Task DeleteByIdAsync(Guid articleId);
    Task EditAsync(EditArticleViewModel viewModel);
    Task<List<Article>> FindAllArticlesAsync(ArticleListViewModel vm);
    Task<List<Article>> FindAllArticlesForUserAsync();
    Task<Article> FindArticleByIdAsync(Guid id);
}
