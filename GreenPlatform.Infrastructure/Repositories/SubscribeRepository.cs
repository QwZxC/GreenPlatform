using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubscribeRepository : BaseRepository<Subscription>, ISubscribeRepository
{
    public SubscribeRepository(GreenPlatformDbContext context) : base(context)
    {
    }

    public async Task<Subscription> FindByWriterIdAndSubscriberIdAsync(Guid writerId, Guid subscriberId)
    {
        return await _context.Subscriptions.FirstOrDefaultAsync(subscription => subscription.WriterId == writerId &&
        subscription.SubscriberId == subscriberId);
    }
}
