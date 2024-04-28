using Domain.Entities;
using Domain.Services;
using Domain.Repositories;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Core;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ArticleService(IArticleRepository articleRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _articleRepository = articleRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateAsync(CreateArticleViewModel viewModel)
    {
        Article article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = viewModel.Title,
            Content = viewModel.Content,
            OwnerId = Guid.Parse(_httpContextAccessor.HttpContext.
                                        User.Claims.First(claim => claim.Type == "UserId").Value)
        };
        _articleRepository.AddEntity(article);
        await _articleRepository.SaveAsync();
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
