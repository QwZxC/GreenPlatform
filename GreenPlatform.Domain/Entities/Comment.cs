using Domain.Entities.Base;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public GreenPlatformUser Creator { get; set; }
    public Article Article { get; set; }
    public Comment? Reply { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatorId { get; set; }
    public Guid ArticleId { get; set; }
    public Guid? ReplyId { get; set; }
}
