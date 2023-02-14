using System.Collections;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Linq;


namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/conversations";
    
    private readonly DataContext context;
    private readonly IUserService userService;
    private static UserModel user;
    private static ICollection<UserChat> chatList = new List<UserChat>();
    private static ICollection<ChatModel> chatModels = new List<ChatModel>();
    public SignalRController(DataContext context, IUserService userService)
    {
        this.context = context;
        this.userService = userService;
    }

    public override async Task OnConnectedAsync()
    {
        var userIdentifier = Context.UserIdentifier;
        if (userIdentifier != null)
        {
            chatList = await context.UserChats.Where(x=> x.UserId == userIdentifier).ToListAsync();
            Console.WriteLine(userIdentifier);
            foreach (var item in chatList)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, item.Chat.Name);
                Console.WriteLine(item.Chat.Name);
            }
            await base.OnConnectedAsync();
        }
    }

    public async Task EnterChat(string chatName)
    {
        await Clients.Groups(chatName).SendAsync("HasEntered", Context.User.Identity.Name);
        await Groups.AddToGroupAsync(Context.ConnectionId, chatName);

        Console.WriteLine($"{Context.User.Identity.Name} connected");
    }
    public async Task ExitChat(string chatName)
    {
        await Clients.Groups(chatName).SendAsync("HasDisconnected", user.UserName);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
        Console.WriteLine($"{Context.User.Identity.Name} disconnected");
    }
    public async Task SendMessage(MessageModel message, string chatName)
    {
        string userName = Context.User.Identity.Name;
        await Clients.Groups(chatName).SendAsync("ReceiveMessage", message);
    }
    public override async Task OnDisconnectedAsync(Exception e)
    {
        var userChat = user.Chats.FirstOrDefault(uc => uc.UserId == user.Id);
        await Clients.Groups(userChat.Chat.Name).SendAsync("HasDisconnected", user.UserName);
        await base.OnDisconnectedAsync(e);
    }


    /*public async Task AddToRoom(string chatName)
    {
        await using (var db = context)
        {
            //Find chat in db
            var chat = db.Chats.FirstOrDefaultAsync(x=> x.Name == chatName);
            if (chat != null)
            {
                var user = await context.Users.FirstOrDefaultAsync(x=> x.UserName == Context.User.Identity.Name);
                UserChat userJoiner = new()
                {
                    UserId = user.Id,
                    ChatId = chat.Id
                    
                };
                db.UserChats.Add(userJoiner);
                db.SaveChanges();
                await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
            }
        }
    }*/

    /*public async Task RemoveFromRoom(string chatName)
    {
        using (var db = context)
        {
            // Retrieve room.
            var chat = db.Chats.FirstOrDefaultAsync(x=> x.Name == chatName);
            if (chat  != null)
            {
                var user = new UserModel() { UserName = Context.User.Identity.Name };
                db.Users.Attach(user);

                chat.Users.Remove(user);
                db.SaveChanges();
                    
                Groups.RemoveFromGroupAsync(Context.ConnectionId, chatName);
            }
        }
    }*/

     

    /*
    //OneonOne-ChatConveration
    public async Task SendPrivateChatMessage(string userId, string user, string message, int chatId,
        bool isChatEncrypted)
    {
        await Clients.Client(userId)
            .SendAsync("RecivePrivateChatMessage", message, user, userId, chatId, isChatEncrypted);
    }
    */


}