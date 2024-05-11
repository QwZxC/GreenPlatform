using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

public class EcologyController : Controller
{
    private readonly ILogger<EcologyController> _logger;
    private readonly IEcologyService _ecologyService;

    public EcologyController(IEcologyService ecologyService, ILogger<EcologyController> logger)
    {
        _ecologyService = ecologyService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> GetAirPolutionInfo(GetAirPolutionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }
        AirPolutionDto dto = await _ecologyService.GetAirPolutionInfoAsync(request);
        if (dto == null)
        {
            ModelState.AddModelError("", "Неверно указано название города");
            return View("Index");
        }
        ViewBag.Check = "true";
        ViewBag.AirPolutionInfo = dto;
        return View("Index");
    }
}
