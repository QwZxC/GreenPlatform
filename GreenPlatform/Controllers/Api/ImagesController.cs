using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
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
}
