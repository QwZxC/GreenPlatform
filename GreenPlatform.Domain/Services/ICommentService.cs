using Domain.Dtos;
using Domain.Entities;

namespace Domain.Services;

public interface ICommentService
{
    Task CreateCommentAsync(CreateCommentViewModel viewModel);
    Task DeleteCommentAsync(Guid commentId);
    Task<List<Comment>> FindCommentsByArticleIdAsync(Guid articleId);
    Task<Comment> FindCommentByIdAsync(Guid commentId);
}