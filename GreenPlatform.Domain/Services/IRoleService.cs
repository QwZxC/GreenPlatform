using Domain.Entities;

namespace Domain.Services;

public interface IRoleService
{
    Task<Role> FindRoleByNameAsync(string name);
}
