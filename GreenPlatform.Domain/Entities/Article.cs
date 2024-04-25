using Domain.Entities.Base;

namespace Domain.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid OwnerId { get; set; }
    public GreenPlatformUser Owner { get; set; }
    public DateTime CreationDate { get; set; }
    public List<Tag> Tags { get; set; }
}