using Domain.Entities;

namespace Domain.Services;

public interface ISubscribeService
{
    Task<List<Subscription>> GetSubscriptionsAsync();

    Task SubscribeAsync(Guid writerId); 
    Task UnSubscribeAsync(Guid writerId); 
}