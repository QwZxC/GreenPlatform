using Domain.Dtos;
using Domain.Entities;

namespace Domain.Services;

public interface IImageService
{
    Task<string?> GetUserAvatarNameAsync(Guid userId);
    Task SaveUserAvatarAsync(EditAccountViewModel model, GreenPlatformUser user);
    void DeletePreviousAvatar(GreenPlatformUser user);
    Task DeleteUserAvatarAsync(Guid userId);
}
