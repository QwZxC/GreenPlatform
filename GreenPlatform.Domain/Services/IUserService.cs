using Domain.Dtos;
using Domain.Entities;
using GreenPlatform.Domain.Dtos;

namespace Domain.Services;

public interface IUserService
{
    Task<UserDto> FindUserByLoginAsync(string login);
    Task<GreenPlatformUser?> FindUserByLoginAndPasswordAsync(string login, string password);
    Task<GreenPlatformUser> CreateUserAsync(string login, string password);
    Task<List<GreenPlatformUser>> FindAllAsync();
    Guid GetAuthorizeUserId();
    Task SaveAsync();
    Task LoginAsync(GreenPlatformUser user);
    Task LogOutAsync();
    Task<UserDto> FindByIdAsync(Guid guid);
    Task EditAccountInfoAsync(EditAccountViewModel model);
    Task<string?> GetUserAvatarNameAsync(Guid userId);
}
