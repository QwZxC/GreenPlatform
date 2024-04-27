using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(GreenPlatformDbContext context) : base(context)
    {
    }

    public async Task<Role> FindRoleByNameAsync(string name)
    {
        return await _context.Role.FirstOrDefaultAsync(role => role.Name == name);
    }

    public async Task<List<Role>> FindUserRolesAsync(GreenPlatformUser user)
    {
        return await _context.Role.Include(role => role.Users)
            .Where(r => r.Users.Contains(user))
            .ToListAsync();
    }
}