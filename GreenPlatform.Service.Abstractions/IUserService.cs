using Domain.Entities;

namespace Service.Abstractions;

public interface IUserService
{
    Task<GreenPlatformUser> FindUserByLoginAsync(string login);
    Task<GreenPlatformUser> FindUserByLoginAndPasswordAsync(string login, string password);
    Task<GreenPlatformUser> CreateUserAsync(string login, string password);
    Task<List<GreenPlatformUser>> FindAllAsync();
    Task SaveAsync();
}
