using System.Collections;
using Microsoft.AspNetCore.SignalR;


namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/conversations";
    
    private readonly DataContext context;

    public SignalRController(DataContext context)
    {
        this.context = context;
        
    }
    
    public async Task SendMessage(string user, string message, string userId)

    {
        await Clients.All.SendAsync("Broadcast", user, message, userId);
    }

    //OneonOne-ChatConveration
    public async Task SendPrivateChatMessage(string userId, string user, string message, int chatId,
        bool isChatEncrypted)
    {
        await Clients.Client(userId)
            .SendAsync("RecivePrivateChatMessage", message, user, userId, chatId, isChatEncrypted);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}