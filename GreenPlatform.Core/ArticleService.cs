using Domain.Entities;
using Domain.Services;
using Domain.Repositories;
using Domain.Dtos;

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
            OwnerId = _userService.GetAuthorizeUserId(),
            CreationDate = DateTime.UtcNow,
        };
        _articleRepository.AddEntity(article);
        await _articleRepository.SaveAsync();
    }

    public async Task DeleteByIdAsync(Guid articleId)
    {
        Article article = await _articleRepository.FindByIdAsync(articleId);
        _articleRepository.Delete(article);
        await _articleRepository.SaveAsync();
    }

    public async Task EditAsync(EditArticleViewModel viewModel)
    {
        Article article = await _articleRepository.FindByIdAsync(viewModel.ArticleId);
        article.Title = viewModel.Title;
        article.Content = viewModel.Content;
        await _articleRepository.SaveAsync();
    }

    public async Task<List<Article>> FindAllArticlesAsync()
    {
        return await _articleRepository.FindAllAsync();
    }

    public async Task<List<Article>> FindAllArticlesForUserAsync()
    {
        return await _articleRepository
            .FindAllArticlesForUserAsync(_userService.GetAuthorizeUserId());
    }

    public async Task<Article> FindArticleByIdAsync(Guid id)
    {
        return await _articleRepository.FindByIdAsync(id);
    }
}
