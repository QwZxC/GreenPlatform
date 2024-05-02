using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

[Route("Error")]
[AllowAnonymous]
public class ErrorController : Controller
{

    public async Task<IActionResult> Error()
    {
        var exceptionFeature = HttpContext?.Features.Get<IExceptionHandlerPathFeature>();
        ErrorViewModel vm = new ();
        if (exceptionFeature != null)
        {
            vm.Message = exceptionFeature.Error.Message;
        }
        return View("Error", vm);
    }
    

    [Route("401")]
    public async Task<IActionResult> HandlePageUnauthorized()
    {
        return RedirectToAction("Login", "Account");
    }
}
