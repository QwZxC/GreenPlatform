using Domain.Dtos;
using Domain.Entities;

namespace Domain.Services;

public interface ITagService
{
    Task CreateTagAsync(CreateTagViewModel vm);
    Task<List<Tag>> FindAllTagsAsync();
    Task<Tag> FindTagByIdAsync(Guid guid);
}
