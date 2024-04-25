using Domain.Entities;

namespace Domain.Repositories;

public interface IRoleRepository : IBaseRepository
{
    Task<Role> FindRoleByNameAsync(string name);
    Task<List<Role>> FindUserRolesAsync(GreenPlatformUser user);
}
