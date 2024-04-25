using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IRoleService roleService;
    public UserService(IUserRepository userRepository, IRoleService roleService)
    {
        this.userRepository = userRepository;
        this.roleService = roleService;
    }

    public async Task<GreenPlatformUser> CreateUserAsync(string login, string password)
    {
        Role role = await roleService.FindRoleByNameAsync("User");
        GreenPlatformUser user = new()
        {
            Login = login,
            Password = password,
            RegistrationDate = DateTime.UtcNow,
            Roles = new List<Role>() { role }
        };
        userRepository.AddEntity(user);
        role.Users.Add(user);
        await userRepository.SaveAsync();
        return user;
    }

    public async Task<GreenPlatformUser> FindUserByLoginAndPasswordAsync(string login, string password)
    {
        GreenPlatformUser user = new GreenPlatformUser() { Login = login, Password = password };
        return await userRepository.FindByLoginAndPassword(user);
    }

    public async Task<GreenPlatformUser> FindUserByLoginAsync(string login)
    {
        return await userRepository.FindByLoginAsync(login);
    }
}
