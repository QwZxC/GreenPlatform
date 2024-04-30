using Domain.Dtos;
using Domain.Entities;

namespace Domain.Services;

public interface IArticleService
{
    Task CreateAsync(CreateArticleViewModel viewModel);
    Task<List<Article>> FindAllArticlesAsync();
    Task<List<Article>> FindAllArticlesForUserAsync();
    Task<Article> FindArticleByIdAsync(Guid id);
}
