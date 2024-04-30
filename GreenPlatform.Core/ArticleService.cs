using Domain.Entities;
using Domain.Services;
using Domain.Repositories;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Core;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserService _userService;

    public ArticleService(IArticleRepository articleRepository,
        IUserService userService)
    {
        _articleRepository = articleRepository;
        _userService = userService;
    }

    public async Task CreateAsync(CreateArticleViewModel viewModel)
    {
        Article article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = viewModel.Title,
            Content = viewModel.Content,
            OwnerId = _userService.GetAuthorizeUserId()
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
