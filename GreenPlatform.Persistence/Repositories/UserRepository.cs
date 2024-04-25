using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{

    public UserRepository(GreenPlatformDbContext context) : base(context)
    {

    }

    public async Task<GreenPlatformUser> FindByLoginAsync(string login)
    {
        return await _context.GreenPlatformUser
            .FirstOrDefaultAsync(dbUser => dbUser.Login == login);
    }

    public async Task<GreenPlatformUser> FindByLoginAndPassword(GreenPlatformUser user)
    {
        return await _context.GreenPlatformUser
            .FirstOrDefaultAsync(dbUser => dbUser.Login == user.Login && dbUser.Password == user.Password);
    }
}
