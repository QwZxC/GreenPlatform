using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    Task<GreenPlatformUser> FindByLoginAndPassword(GreenPlatformUser user);
    Task<GreenPlatformUser> FindByLoginAsync(string login);
}
