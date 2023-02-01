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

    public async Task SendMessage(string user, string message, string userId)
    {
        var chat = context.Chats.FirstOrDefault(c => c.UserId == userId);
        await Clients.Client(chat.UserId).SendAsync("BroadCast", user, message);
        //  encryptionController.Encrypt(message);
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
    //hash.HashPassword(userModel, messageModel.Message);
    /*public async Task<ActionResult> Test()
    {
    }*/
}