using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Domain.Repositories;

namespace Core;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserService _userService; 

    public CommentService(ICommentRepository commentRepository, IUserService userService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
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
        if (comment.CreatorId == _userService.GetAuthorizeUserId())
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
}
