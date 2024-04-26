using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly IPasswordHasher _passwordHasher;
    public UserService(IUserRepository userRepository, IRoleService roleService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleService = roleService;
        _passwordHasher = passwordHasher;
    }

    public async Task<GreenPlatformUser> CreateUserAsync(string login, string password)
    {
        Role role = await _roleService.FindRoleByNameAsync("User");
        string hashedPassword = _passwordHasher.Generate(password);
        GreenPlatformUser user = new()
        {
            Id = Guid.NewGuid(),
            Login = login,
            Password = hashedPassword,
            RegistrationDate = DateTime.UtcNow,
            Roles = new List<Role>() { role }
        };
        _userRepository.AddEntity(user);
        role.Users.Add(user);
        return user;
    }

    public async Task<List<GreenPlatformUser>> FindAllAsync()
    {
        return await _userRepository.FindAllAsync();
    }

    public async Task<GreenPlatformUser> FindUserByLoginAndPasswordAsync(string login, string password)
    {
        GreenPlatformUser user = await _userRepository.FindByLoginAsync(login);

        if (_passwordHasher.Verify(password, user.Password))
        {
            return user;
        }

        return null;
    }

    public async Task<GreenPlatformUser> FindUserByLoginAsync(string login)
    {
        return await _userRepository.FindByLoginAsync(login);
    }

    public async Task SaveAsync()
    {
        await _userRepository.SaveAsync();
    }
}
