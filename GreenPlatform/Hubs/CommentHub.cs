using Domain.Dtos;
using Domain.Entities;
using Domain.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GreenPlatform.Hubs;

public sealed class CommentHub : Hub
{
    private readonly ICommentService _commentService;
        
    public CommentHub(ICommentService commentService, GreenPlatformDbContext contex)
    {
        _commentService = commentService;
    }

    public async Task JoinGroup(Guid articleId)
    {
        List<Comment> comments = await _commentService.FindCommentsByArticleIdAsync(articleId);
        await Groups.AddToGroupAsync(Context.ConnectionId, articleId.ToString());
        await Clients.Caller.SendAsync("JoinGroup", comments);
    }

    [Authorize]
    public async Task SendComment(CreateCommentViewModel comment)
    {
        if (string.IsNullOrWhiteSpace(comment.Content))
        {
            return;
        }
        await _commentService.CreateCommentAsync(comment);
        await Clients
            .Group(comment.ArticleId.ToString())
            .SendAsync("ReceiveMessage", await _commentService.FindLastUserCommentForArticleAsync(comment));
    }

    [Authorize]
    public async Task DeleteComment(Guid commentGuid)
    {
        Comment comment = await _commentService.FindCommentByIdAsync(commentGuid);
        await Clients
            .Group(comment.ArticleId.ToString())
            .SendAsync("DeleteComment", commentGuid);
    }
}