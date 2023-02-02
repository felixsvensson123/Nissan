using System.Collections;
using Microsoft.AspNetCore.SignalR;


namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/conversations";
    /*
    private MessageController messageController { get; set; }
    private EncryptionController encryptionController { get; set; }
    */

    private readonly DataContext context;

    //private IPasswordHasher<UserModel> hash;
    //private HashAlgorithm hash;

    public SignalRController(DataContext context)
    {
        this.context = context;
        //this.messageController = messageController;
        //this.hash = hash;
        // this.encryptionController = encryptionController;
    }
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        await SendMessage($"{Context.User}", " has joined the chat.", "");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
    public async Task SendMessage(string user, string message, string userId)
    {
        await Clients.All.SendAsync("Broadcast", user, message, userId);
    }


    //hash.HashPassword(userModel, messageModel.Message);
    /*public async Task<ActionResult> Test()
    {
    }*/
}