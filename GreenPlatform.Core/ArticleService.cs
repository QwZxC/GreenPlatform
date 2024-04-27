using Domain.Entities;
using Domain.Services;
using Domain.Repositories;

namespace Core;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<Article>> FindAllArticlesAsync()
    {
        return await _articleRepository.FindAllAsync();
    }

    public async Task<Article> FindArticleByIdAsync(Guid id)
    {
        return await _articleRepository.FindArticleByIdAsync(id);
    }
}
