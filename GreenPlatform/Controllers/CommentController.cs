using Domain.Dtos;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public IActionResult CreateComment()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComment(CreateCommentViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return ReturnToArticle(viewModel.ArticleId);
        }
        await _commentService.CreateCommentAsync(viewModel);
        return ReturnToArticle(viewModel.ArticleId);
    }

    [NonAction]
    private IActionResult ReturnToArticle(Guid articleId)
    {
        return RedirectToAction("Article", "Article", new { articleId });
    }
}
