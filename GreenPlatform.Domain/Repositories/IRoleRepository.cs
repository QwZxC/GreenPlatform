using Domain.Entities;

namespace Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<Role> FindRoleByNameAsync(string name);
    Task<List<Role>> FindUserRolesAsync(GreenPlatformUser user);
}
