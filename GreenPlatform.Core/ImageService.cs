using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using GreenPlatform.Domain.Dtos;
using Microsoft.AspNetCore.Hosting;

namespace Core;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUserService _userService;

    public ImageService(IWebHostEnvironment webHostEnvironment, IUserService userService)
    {
        _webHostEnvironment = webHostEnvironment;
        _userService = userService;
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
        UserDto user = await _userService.FindByIdAsync(userId);
        return user.AvatarPath;
    }
}
