﻿using System.Collections;
using Microsoft.AspNetCore.SignalR;


namespace N_Chat.Server.Controllers;

public class SignalRController : Hub

{
    public const string HubUrl = "/conversations";
    
    private readonly DataContext context;
    private readonly static UserController userController;
    public SignalRController(DataContext context)
    {
        this.context = context;
    }
    public override async Task OnConnectedAsync()
    {
        //Get users
        var user = await userController.GetUserWithIncludes(Context.User.Identity.Name);
      //      await context.Users.Include(u => u.Chats).ThenInclude(c => c.Chat).FirstOrDefaultAsync(x => x.UserName == Context.User.Identity.Name);
//Add user to each assigned group
        if (user != null)
        {
            foreach (var item in user.Chats)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, item.Chat.Name);
            }
            Console.WriteLine($"{Context.ConnectionId} connected");
            await base.OnConnectedAsync();
        }
    }

    public override Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        return base.OnDisconnectedAsync(e);
    }

    
    public async Task AddToRoom(string chatName)
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
    }
    public async Task SendGroupMessage(string userName,string message, string chatName)
    {
        await Clients.Groups(chatName).SendAsync("SendGroupMessage",userName, message, chatName);
    }
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