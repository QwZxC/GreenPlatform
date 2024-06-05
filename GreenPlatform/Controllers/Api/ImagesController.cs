using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : Controller
{
    private readonly IImageService _imageSrvice;

    public ImagesController(IImageService imageService)
    {
        _imageSrvice = imageService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserAvatarName(Guid userId)
    {
        string avatarPath = await _imageSrvice.GetUserAvatarNameAsync(userId);
        return !string.IsNullOrWhiteSpace(avatarPath) ? Ok(avatarPath) : NotFound("empty-avatar.jpg");
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> DeleteUserAvatar(Guid userId)
    {
        await _imageSrvice.DeleteUserAvatarAsync(userId);
        return Redirect(Request.Headers.Referer.ToString());
    }
}
