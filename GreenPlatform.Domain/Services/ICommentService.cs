using Domain.Dtos;

namespace Domain.Services;

public interface ICommentService
{
    Task CreateCommentAsync(CreateCommentViewModel viewModel);
}