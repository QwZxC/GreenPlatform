using Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Subscription : BaseEntity
{
    public Guid? WriterId { get; set; }
    public Guid? SubscriberId { get; set; }
    [ForeignKey(nameof(WriterId))]
    [InverseProperty("Subscribers")]
    public GreenPlatformUser? Writer { get; set; }
    [ForeignKey(nameof(SubscriberId))]
    [InverseProperty("Subscriptions")]
    public GreenPlatformUser? Subscriber { get; set; }
    public DateTime SubscriptionDate { get; set; }
}
