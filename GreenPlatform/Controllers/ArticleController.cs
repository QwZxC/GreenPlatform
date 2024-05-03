using Domain.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

[Route("articles")]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ICommentService _commentService;
    private readonly ILogger<ArticleController> _logger;

    public ArticleController(
        IArticleService articleService,
        ICommentService commentService,
        ILogger<ArticleController> logger
        )
    {
        _articleService = articleService;
        _commentService = commentService;
        _logger = logger;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Articles()
    {
        var viewModel = new ArticleListViewModel()
        {
            Articles = await _articleService.FindAllArticlesAsync()
        };
        return View(viewModel);
    }

    [Route("my")]
    [Authorize]
    public async Task<IActionResult> MyArticles()
    {
        Log("Получение списка статей");
        var viewModel = new ArticleListViewModel()
        {
            Articles = await _articleService.FindAllArticlesForUserAsync()
        };
        return View(viewModel);
    }

    [Route("{articleId}")]
    [AllowAnonymous]
    public async Task<IActionResult> Article(Guid articleId)
    {
        Log($"Получение подробного просмотра статьи {articleId}");
        var viewModel = new SelectedArticleViewModel()
        {
            SelectedArticle = await _articleService.FindArticleByIdAsync(articleId),
            Comments = await _commentService.FindCommentsByArticleIdAsync(articleId)
        };
        return View(viewModel);
    }

    [HttpGet]
    [Authorize]
    [Route("new")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [Route("new")]
    public async Task<IActionResult> Create(CreateArticleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _articleService.CreateAsync(viewModel);
        Log($"Создана статья {viewModel.Title}");
        return RedirectToAction("MyArticles");
    }

    [Authorize]
    [HttpGet]
    [Route("edit/{articleId}")]
    public async Task<IActionResult> Edit(Guid articleId)
    {
        var viewModel = new EditArticleViewModel(
            await _articleService.FindArticleByIdAsync(articleId));
        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    [Route("edit/{articleId}")]
    public async Task<IActionResult> Edit(EditArticleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Log($"Изменена статья{viewModel.ArticleId}");
        await _articleService.EditAsync(viewModel);
        return RedirectToAction("MyArticles");
    }

    [HttpPost]
    [Authorize]
    [Route("{articleId}")]
    public async Task<IActionResult> Delete(Guid articleId)
    {
        await _articleService.DeleteByIdAsync(articleId);
        Log($"Удалена статья {articleId}");
        return RedirectToAction("MyArticles");
    }

    [NonAction]
    private void Log(string action)
    {
        _logger.LogInformation("{Action}\n" +
                "\t\tВремя: {Time}\n" +
                "\t\tДата: {Date}\n",
            action, DateTime.Now.ToShortTimeString(), DateTime.Now.ToShortDateString());
    }
}
