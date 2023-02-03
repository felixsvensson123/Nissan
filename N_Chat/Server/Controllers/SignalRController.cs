using System.Collections;
using Microsoft.AspNetCore.SignalR;


namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/conversations";
    private readonly ChatController chatController;
    private readonly MessageController messageController;
    private readonly EncryptionController encryptionController;
    private readonly UserController userController;
    private readonly DataContext context;
    public SignalRController(DataContext context,ChatController chatController,MessageController messageController,UserController userController,EncryptionController encryptionControlle)
    {
        this.context = context;
        this.chatController = chatController;
        this.messageController = messageController;
        this.userController = userController;
        this.encryptionController = encryptionController;
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
//OneonOne-ChatConveration
    public  async Task SendPrivateChatMessage(string userId,string user,string message,int chatId ,string chatName)
    {
        await chatController.GetById(chatId);
        await Clients.Client(userId).SendAsync("RecivePrivateChatMessage",message, user, chatId,userId,chatName);
    }


    //EncryptChat-Converation
    public async Task EncryptChat(string userId,string user,int chatId,string chatName,bool? isEncrypted)
    {
        var chat = await chatController.GetById(chatId);
        if (chat != null)
        {
            await Clients.Client(userId).SendAsync("ReciveEncryptChat",chat,chat.Id,chat.UserId,user,chatName, chat.IsChatEncrypted);
        }

    }
    //
    public async Task EncryptIndividualMessage(int messageId,bool isEncrypted,string user,string userId,string message)
    {
        var encryptMessage = await messageController.GetById(messageId);
        if(encryptMessage != null)
        {
            await Clients.Client(userId).SendAsync("ReciveEncryptMessage",message,isEncrypted,user,userId,messageId);
        }
    }

    //hash.HashPassword(userModel, messageModel.Message);
    /*public async Task<ActionResult> Test()
    {
    }*/
}