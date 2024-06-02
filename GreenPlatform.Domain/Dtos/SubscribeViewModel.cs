namespace Domain.Dtos;

public class SubscribeViewModel
{
    public Guid? WriterId { get; set; }
    public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;
}