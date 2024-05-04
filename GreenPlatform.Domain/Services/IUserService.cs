using Domain.Entities;

namespace Domain.Services;

public interface IUserService
{
    Task<GreenPlatformUser> FindUserByLoginAsync(string login);
    Task<GreenPlatformUser?> FindUserByLoginAndPasswordAsync(string login, string password);
    Task<GreenPlatformUser> CreateUserAsync(string login, string password);
    Task<List<GreenPlatformUser>> FindAllAsync();
    Guid GetAuthorizeUserId();
    Task SaveAsync();
    Task LoginAsync(GreenPlatformUser user);
    Task LogOutAsync();
}
