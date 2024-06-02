using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Domain.Repositories;

namespace Core;

public class SubscribeService : ISubscribeService
{
    private readonly ISubscribeRepository _subscribeRepository;
    private readonly IUserService _userService;
    public SubscribeService(ISubscribeRepository subscribeRepository,
        IUserService userService)
    {
        _subscribeRepository = subscribeRepository;
        _userService = userService;
    }

    public async Task<List<Subscription>> GetSubscriptionsAsync()
    {
        return await _subscribeRepository.FindAllAsync();
    }

    public async Task SubscribeAsync(Guid writerId)
    {
        Subscription subscription = new Subscription()
        {
            Id = Guid.NewGuid(),
            SubscriberId = _userService.GetAuthorizeUserId(),
            WriterId = writerId,
            SubscriptionDate = DateTime.UtcNow
        };

        _subscribeRepository.AddEntity(subscription);
        await _subscribeRepository.SaveAsync();
    }

    public async Task UnSubscribeAsync(Guid writerId)
    {
        Subscription subscription = await _subscribeRepository.FindByWriterIdAndSubscriberIdAsync(writerId, _userService.GetAuthorizeUserId());
        _subscribeRepository.Delete(subscription);
        await _subscribeRepository.SaveAsync();
    }
}
