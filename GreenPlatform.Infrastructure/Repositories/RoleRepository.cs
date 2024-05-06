using Domain.Entities;
using Domain.Repositories;
using Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация IRoleRepository
/// Так же реализует в себе IBaseRepository,
/// путём наследования от BaseRepository
/// </summary>
public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(GreenPlatformDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Осуществляет поиск роли по имени, если таковой нет, то выкидывает исключение
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<Role> FindRoleByNameAsync(string name)
    {
        return await _context.Role.FirstOrDefaultAsync(role => role.Name == name)
            ?? throw new NotFoundException($"Роль {name} не найдена");
    }
}