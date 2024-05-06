using Common.Exceptions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Реализация IUserRepository.
/// Так же реализует в себе IBaseRepository,
/// путём наследования от BaseRepository
/// </summary>
public class UserRepository : BaseRepository<GreenPlatformUser>, IUserRepository
{

    public UserRepository(GreenPlatformDbContext context) : base(context)
    {

    }

    /// <summary>
    /// В случае не нахождения подходящего элемента выкидывает исключение
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<GreenPlatformUser> FindByLoginAsync(string login)
    {
        return await _context.GreenPlatformUser.Include(user => user.Roles)
            .FirstOrDefaultAsync(dbUser => dbUser.Login == login) 
            ?? throw new NotFoundException($"Пользователь {login} не найден");
    }
}
