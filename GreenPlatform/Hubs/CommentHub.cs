using Domain.Dtos;
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
        await Groups.AddToGroupAsync(Context.ConnectionId, articleId.ToString());
        await Clients.Caller.SendAsync("JoinGroup", await _commentService.FindCommentsByArticleIdAsync(articleId));
    }

    [Authorize]
    public async Task SendComment(CreateCommentViewModel comment)
    {
        await _commentService.CreateCommentAsync(comment);
        await Clients
            .Group(comment.ArticleId.ToString())
            .SendAsync("ReceiveMessage", await _commentService.FindLastUserCommentForArticleAsync(comment));
    }
}