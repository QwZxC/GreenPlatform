using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository : IBaseRepository<GreenPlatformUser>
{
    Task<GreenPlatformUser> FindByLoginAsync(string login);
}
