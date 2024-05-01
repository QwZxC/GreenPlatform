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

    [HttpPost]
    [Authorize]
    [Route("{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid commentId, Guid articleId)
    {
        await _commentService.DeleteCommentAsync(commentId);
        return ReturnToArticle(articleId);
    }

    [NonAction]
    private IActionResult ReturnToArticle(Guid articleId)
    {
        return RedirectToAction("Article", "Article", new { articleId });
    }
}
