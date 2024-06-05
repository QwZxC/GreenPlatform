using Domain.Entities;
using Domain.Services;
using Domain.Repositories;
using Domain.Dtos;

namespace Core;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUserService _userService;
    private readonly ITagService _tagService;

    public ArticleService(IArticleRepository articleRepository,
        IUserService userService,
        ITagService tagService)
    {
        _articleRepository = articleRepository;
        _userService = userService;
        _tagService = tagService;
    }

    public async Task CreateAsync(CreateArticleViewModel viewModel)
    {
        Article article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = viewModel.Title,
            Content = viewModel.Content,
            Tags = await GetTagsAsync(viewModel.TagGuids),
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
        article.Tags = await GetTagsAsync(viewModel.TagGuids);
        _articleRepository.Update(article);
        await _articleRepository.SaveAsync();
    }

    public async Task<List<Article>> FindAllArticlesAsync(ArticleListViewModel vm)
    {
        return await _articleRepository.FindAllArticelsByTitle(vm);
    }

    public async Task<List<Article>> FindAllArticlesAsync(ArticleListViewModel vm, bool bySubscription)
    {
        Guid authorizeUserId = _userService.GetAuthorizeUserId();
        List<Article> articles = await _articleRepository.FindAllArticelsByTitle(vm);
        return articles.FindAll(article => article.Owner.Subscribers.Exists(subscribe => subscribe.SubscriberId == authorizeUserId));
        
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
    private async Task<List<Tag>> GetTagsAsync(List<Guid> tagGuids)
    {
        List<Tag> tags = new List<Tag>();
        tagGuids.ForEach(async guid => tags.Add(await _tagService.FindTagByIdAsync(guid)));
        return tags;
    }
}
