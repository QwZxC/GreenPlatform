using Domain.Repositories;
using Service.Abstractions;

namespace Services;

public sealed class TagService : ITagService
{
    private readonly ITagRepository tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
    }
}