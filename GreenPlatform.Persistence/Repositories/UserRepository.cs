using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<GreenPlatformUser>, IUserRepository
{

    public UserRepository(GreenPlatformDbContext context) : base(context)
    {

    }

    public async Task<GreenPlatformUser> FindByLoginAsync(string login)
    {
        return await _context.GreenPlatformUser.Include(user => user.Roles)
            .FirstOrDefaultAsync(dbUser => dbUser.Login == login);
    }
}
