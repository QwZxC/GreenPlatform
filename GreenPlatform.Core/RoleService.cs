using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Core;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> FindRoleByNameAsync(string name)
    {
        return await _roleRepository.FindRoleByNameAsync(name);
    }
}
