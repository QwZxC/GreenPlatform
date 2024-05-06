using Domain.Entities;

namespace Domain.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью GreenPlatformUser
/// </summary>
public interface IUserRepository : IBaseRepository<GreenPlatformUser>
{
    /// <summary>
    /// Осуществляет поиск пользователя по логину
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    Task<GreenPlatformUser> FindByLoginAsync(string login);
}
