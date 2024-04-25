using Domain.Entities.Base;

namespace Domain.Entities;

public class Subscription : BaseEntity
{
    public Guid? WriterId { get; set; }
    public Guid? SubscriberId { get; set; }
    public GreenPlatformUser? Writer { get; set; }
    public GreenPlatformUser? Subscriber { get; set; }
    public DateTime SubscriptionDate { get; set; }
}
