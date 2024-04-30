using Domain.Dtos;
using Domain.Services;
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
    public async Task<IActionResult> CreateComment(CreateCommentViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return PartialView();
        }
        await _commentService.CreateCommentAsync(viewModel);

        return RedirectToAction("Article", "Article", new { articleId = viewModel.ArticleId });
    }
}
