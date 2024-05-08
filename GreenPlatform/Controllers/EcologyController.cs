using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

public class EcologyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
