using System.Collections;
using Microsoft.AspNetCore.SignalR;


namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/conversations";
   
    private readonly ChatController _chatController;
    private readonly MessageController _messageController;  
    private readonly EncryptionController _encryptionController;    
    private readonly UserController _userController;    

   
    private readonly DataContext context;
    private string encryptedMessage;

    //private IPasswordHasher<UserModel> hash;
    //private HashAlgorithm hash;

    public SignalRController(DataContext context,ChatController chatController,MessageController messageController,UserController userController,EncryptionController encryptionController)
    {
        this.context = context;
        this._chatController = chatController;  
        this._messageController = messageController;    
        this._userController = userController;  
        this._encryptionController = encryptionController;  
       
        //this.hash = hash;
      
    }







    //UserSendsChatMessage -to all users
    public async Task SendMessage(string user, string message, string userId)

    {
        
        await Clients.Client(userId).SendAsync("Broadcast", user, message, userId);
        //  encryptionController.Encrypt(message);
    }

    //OneonOne-ChatConveration
    public  async Task SendPrivateChatMessage(string userId,string user,string message,int chatId,bool isChatEncrypted)
    {
        await _chatController.GetById(chatId);
        await Clients.Client(userId).SendAsync("RecivePrivateChatMessage",message, user,userId,chatId, isChatEncrypted);
    }

    public async Task EncryptChat(string message,bool IsChatEncrypted)
    {
        if (IsChatEncrypted)
        {
            message = _encryptionController.Encrypt(message);
        }
        
      
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