using Domain.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;

namespace Core;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserRepository _userRepository;

    public ImageService(IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
    {
        _webHostEnvironment = webHostEnvironment;
        _userRepository = userRepository;
    }

    public async Task SaveUserAvatarAsync(EditAccountViewModel model, GreenPlatformUser user)
    {
        string fileName = Path.GetFileName(model.ProfileImage.FileName);
        string extensions = Path.GetExtension(model.ProfileImage.FileName);
        if (!string.IsNullOrWhiteSpace(user.AvatarPath))
        {
            DeletePreviousAvatar(user);
        }
        user.AvatarPath = fileName + user.Id + extensions;
        string path = Path.Combine(_webHostEnvironment.WebRootPath + "/image/" + user.AvatarPath);
        using var fileStream = new FileStream(path, FileMode.Create);
        await model.ProfileImage.CopyToAsync(fileStream);
    }

    public void DeletePreviousAvatar(GreenPlatformUser user)
    {
        string pathToDelete = Path.Combine(_webHostEnvironment.WebRootPath + "/image/" + user.AvatarPath);
        File.Delete(pathToDelete);
    }

    public async Task<string?> GetUserAvatarNameAsync(Guid userId)
    {
        GreenPlatformUser user = await _userRepository.FindByIdAsync(userId);
        return user.AvatarPath;
    }

    public async Task DeleteUserAvatarAsync(Guid userId)
    {
        GreenPlatformUser user = await _userRepository.FindByIdAsync(userId);
        DeletePreviousAvatar(user);
        user.AvatarPath = null;
        _userRepository.Update(user);
        await _userRepository.SaveAsync();
    }
}
