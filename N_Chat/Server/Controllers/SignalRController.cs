using System.Security.Cryptography;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using N_Chat.Server.Controllers;
using System.Security.Cryptography;

namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/chat";
    private MessageController messageController { get; set; }
    private EncryptionController encryptionController { get; set; }

    private readonly DataContext context;

    //private IPasswordHasher<UserModel> hash;
    private HashAlgorithm hash;

    public SignalRController(MessageController messageController, DataContext context, HashAlgorithm hash,
        EncryptionController encryptionController)
    {
        this.context = context;
        this.messageController = messageController;
        this.hash = hash;
        this.encryptionController = encryptionController;
    }

    public async Task SendMessage(UserModel userModel, MessageModel messageModel)
    {
        await Clients.All.SendAsync("BroadCast", userModel, messageModel.Message);
        encryptionController.Encrypt(messageModel.Message);
        await messageController.PostMessage(messageModel);
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