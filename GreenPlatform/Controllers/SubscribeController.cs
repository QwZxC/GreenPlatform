using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

[Authorize]
public class SubscribeController : Controller
{
    private readonly ISubscribeService _subscribeService;

    public SubscribeController(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(Guid writerId)
    {
        await _subscribeService.SubscribeAsync(writerId);
        return Redirect(Request.Headers.Referer.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> UnSubscribe(Guid writerId)
    {
        await _subscribeService.UnSubscribeAsync(writerId);
        return Redirect(Request.Headers.Referer.ToString());
    }
}