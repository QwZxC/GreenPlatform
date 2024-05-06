using Domain.Entities;

namespace Domain.Repositories;

/// <summary>
/// Репозиторий для работы с сущностью Role
/// </summary>
public interface IRoleRepository : IBaseRepository<Role>
{
    /// <summary>
    /// Осуществляет поиск роли по имении
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<Role> FindRoleByNameAsync(string name);
}
