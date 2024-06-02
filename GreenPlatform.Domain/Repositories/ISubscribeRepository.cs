using Domain.Entities;

namespace Domain.Repositories;

public interface ISubscribeRepository : IBaseRepository<Subscription>
{
    Task<Subscription> FindByWriterIdAndSubscriberIdAsync(Guid writerId, Guid subscriberId);
}