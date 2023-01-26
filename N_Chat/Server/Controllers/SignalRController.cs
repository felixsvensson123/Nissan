using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using N_Chat.Server.Controllers;
namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    private MessageController messageController { get; set; }
    private readonly DataContext context;
    public SignalRController(MessageController messageController, DataContext context)
    {
        this.context = context;
        this.messageController = messageController;
        
    }
    public async Task SendMessage(UserModel user, MessageModel message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message.Message);
        messageController.PostUserMessage(message);
    }
    /*public async Task<ActionResult> Test()
    {
    }*/
    
}