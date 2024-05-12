using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

public class TagController : Controller
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    public async Task<IActionResult> TagList()
    {
        ViewBag.TagList = await _tagService.FindAllTagsAsync();
        return View();
    }

    public async Task<IActionResult> CreateTag()
    {
        ViewBag.Check = true;
        ViewBag.TagList = await _tagService.FindAllTagsAsync();
        return View("TagList");
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(CreateTagViewModel vm)
    {
        if(!ModelState.IsValid)
        {
            ViewBag.TagList = await _tagService.FindAllTagsAsync();
            ViewBag.Check = true;
            return View("TagList");
        }
        await _tagService.CreateTagAsync(vm);
        ViewBag.TagList = await _tagService.FindAllTagsAsync();
        return View("TagList");
    }
}
