using Domain.Repositories;
using Domain.Services;

namespace Core;

public sealed class TagService : ITagService
{
    private readonly ITagRepository tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
    }
}