using Domain.Services;
using GreenPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

[Route("articles")]
[AllowAnonymous]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<IActionResult> Articles()
    {
        var viewModel= new ArticleListViewModel()
        {
            Articles = await _articleService.FindAllArticlesAsync()
        };
        return View(viewModel);
    }

    [Route("{articleId}")]
    public async Task<IActionResult> Article(Guid articleId)
    {
        var viewModel = new SelectedArticleViewModel()
        {
            SelectedArticle = await _articleService.FindArticleByIdAsync(articleId)
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        return View();
    }
}
