using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository<GreenPlatformUser>
{
    Task<GreenPlatformUser> FindByLoginAndPassword(GreenPlatformUser user);
    Task<GreenPlatformUser> FindByLoginAsync(string login);
}
