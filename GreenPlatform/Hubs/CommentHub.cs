using Microsoft.AspNetCore.SignalR;

namespace GreenPlatform.Hubs;

public sealed class CommentHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }
}