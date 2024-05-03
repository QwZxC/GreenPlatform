using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenPlatform.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly ILogger<CommentController> _logger;

    public CommentController(ICommentService commentService, 
        ILogger<CommentController> logger)
    {
        _commentService = commentService;
        _logger = logger;
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
        Log($"Создание комментария {viewModel.Content} под статьёй {viewModel.ArticleId}");
        await _commentService.CreateCommentAsync(viewModel);
        return ReturnToArticle(viewModel.ArticleId);
    }

    [HttpPost]
    [Authorize]
    [Route("{commentId}")]
    public async Task<IActionResult> DeleteComment(Guid commentId, Guid articleId)
    {
        Comment comment = await _commentService.FindCommentByIdAsync(commentId);
        Log($"Удаление комментария {comment.Content} под статьёй {articleId}");
        await _commentService.DeleteCommentAsync(commentId);
        return ReturnToArticle(articleId);
    }

    [NonAction]
    private IActionResult ReturnToArticle(Guid articleId)
    {
        return RedirectToAction("Article", "Article", new { articleId });
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
