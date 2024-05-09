using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Domain.Repositories;

namespace Core;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IArticleService _articleService;
    private readonly IUserService _userService;

    public CommentService(ICommentRepository commentRepository, IUserService userService, IArticleService articleService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
        _articleService = articleService;
    }

    public async Task CreateCommentAsync(CreateCommentViewModel viewModel)
    {
        Comment comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Content = viewModel.Content,
            ArticleId = viewModel.ArticleId,
            CreationDate = DateTime.UtcNow,
            CreatorId = _userService.GetAuthorizeUserId()
        };
        _commentRepository.AddEntity(comment);
        await _commentRepository.SaveAsync();
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        Comment comment = await _commentRepository.FindByIdAsync(commentId);
        Article article = await _articleService.FindArticleByIdAsync(comment.ArticleId);
        Guid authorizeUserId = _userService.GetAuthorizeUserId();
        if (comment.CreatorId == authorizeUserId || 
             article.OwnerId == authorizeUserId)
        {
            _commentRepository.Delete(comment);
            await _commentRepository.SaveAsync();
        }
    }

    public async Task<List<Comment>> FindCommentsByArticleIdAsync(Guid articleId)
    {
        return await _commentRepository.FindCommentsByArticleId(articleId);
    }

    public async Task<Comment> FindCommentByIdAsync(Guid commentId)
    {
        return await _commentRepository.FindByIdAsync(commentId);
    }

    public async Task<Comment> FindLastUserCommentForArticleAsync(CreateCommentViewModel comment)
    {
        return await _commentRepository
            .FindLastUserCommentForArticleAsync(comment.ArticleId, _userService.GetAuthorizeUserId());
    }
}
