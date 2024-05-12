using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Core;

public sealed class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task CreateTagAsync(CreateTagViewModel vm)
    {
        Tag tag = new Tag
        {
            Id = Guid.NewGuid(),
            Name = vm.Name
        };
        _tagRepository.AddEntity(tag);
        await _tagRepository.SaveAsync();
    }

    public async Task<List<Tag>> FindAllTagsAsync()
    {
        return await _tagRepository.FindAllAsync();
    }

    public async Task<Tag> FindTagByIdAsync(Guid guid)
    {
        return await _tagRepository.FindByIdAsync(guid);
    }
}