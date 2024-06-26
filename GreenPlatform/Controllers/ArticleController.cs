﻿using Domain.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Entities;

namespace GreenPlatform.Controllers;

[Route("articles")]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ICommentService _commentService;
    private readonly ITagService _tagService;
    private readonly ILogger<ArticleController> _logger;

    public ArticleController(
        IArticleService articleService,
        ICommentService commentService,
        ILogger<ArticleController> logger,
        ITagService tagService)
    {
        _articleService = articleService;
        _commentService = commentService;
        _logger = logger;
        _tagService = tagService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Articles([FromQuery] ArticleListViewModel vm)
    {
        if (string.IsNullOrWhiteSpace(vm.Title))
        {
            vm.Title = "";
        }
        List<SelectListItem> propertiesToSelect = [new() { Text = "Названию", Value = "Title" },
                                                   new () { Text = "Дате", Value = "CreationDate" } ];
        ViewBag.PropertiesToSelect = propertiesToSelect;
        ViewBag.Articles = await _articleService.FindAllArticlesAsync(vm);
        ViewBag.ArticlesBySubscription = new List<Article>();
        
        if (!string.IsNullOrWhiteSpace(Request.Cookies["Authorization"]))
        {
            ViewBag.ArticlesBySubscription = await _articleService.FindAllArticlesAsync(vm, vm.BySubscription);
        }
        return View();
    }

    [Route("my")]
    [Authorize]
    public async Task<IActionResult> MyArticles()
    {
        Log("Получение списка статей");
        ViewBag.Articles = await _articleService.FindAllArticlesForUserAsync();
        return View();
    }

    [Route("{articleId}")]
    [AllowAnonymous]
    public async Task<IActionResult> Article(Guid articleId)
    {
        Log($"Получение подробного просмотра статьи {articleId}");
        var viewModel = new SelectedArticleViewModel()
        {
            SelectedArticle = await _articleService.FindArticleByIdAsync(articleId)
        };
        return View(viewModel);
    }

    [HttpGet]
    [Authorize]
    [Route("new")]
    public async Task<IActionResult> Create()
    {
        var reffer = Request.Headers["Referer"].ToString();
        ViewBag.TagsToSelect = await GetTagsAsync();
        if (reffer != null)
        {
            ViewData["Reffer"] = reffer;
        }
        return View();
    }

    [HttpPost]
    [Authorize]
    [Route("new")]
    public async Task<IActionResult> Create(CreateArticleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.TagsToSelect = await GetTagsAsync();
            return View();
        }
        await _articleService.CreateAsync(viewModel);
        Log($"Создана статья {viewModel.Title}");
        return Redirect(viewModel.PreviousUrl);
    }

    [Authorize]
    [HttpGet]
    [Route("edit/{articleId}")]
    public async Task<IActionResult> Edit(Guid articleId)
    {
        var viewModel = new EditArticleViewModel(
            await _articleService.FindArticleByIdAsync(articleId));
        ViewBag.TagsToSelect = await GetTagsAsync();
        return View(viewModel);
    }

    [NonAction]
    private async Task<List<SelectListItem>> GetTagsAsync()
    {
        List<SelectListItem> tagsToSelect = new List<SelectListItem>();
        List<Tag> tags = await _tagService.FindAllTagsAsync();
        tags.ForEach(tag => tagsToSelect.Add(new SelectListItem() { Text = tag.Name, Value = tag.Id.ToString() }));
        return tagsToSelect;
    }

    [HttpPost]
    [Authorize]
    [Route("edit/{articleId}")]
    public async Task<IActionResult> Edit(EditArticleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.TagsToSelect = await GetTagsAsync();
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
