 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;

namespace N_Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly DataContext context;

        public ChatController(DataContext context)
        {
            this.context = context;
        }


        //[HttpGet("getall")]
        //public async Task<IEnumerable<ChatModel>> GetAll()
        //{
        //    List<ChatModel> chatList = await context.Chats.OfType<ChatModel>().Where(c => c.IsChatEnded != true).ToListAsync();
        //    return chatList;
        //}


        [HttpGet("getall")]
        public async Task<ICollection<ChatModel>> GetIncludedChats()
        {
            var dbChats = await context.Chats
                .Include(u => u.Users)
                .ThenInclude(uc => uc.User)
                .Where(u => u.IsChatEnded != true)
                .AsSingleQuery()
                .ToListAsync();
            return dbChats;
        }


        [HttpGet("getchatbyid/{id}")]
        public async Task<ChatModel> GetById(int id)
        {
            var result = await GetIncludedChats(); 
            return result.FirstOrDefault(x => x.Id == id);
        }

        [HttpPut("updatechat/{id}")]
        public async Task<ActionResult> UpdateIsDeleted(ChatModel chat, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ChatModel chatToBeUpdated = await GetById(id);
            chatToBeUpdated.IsChatEnded = chat.IsChatEnded;
            chatToBeUpdated.ChatEnded = chat.ChatEnded;
            chatToBeUpdated.IsChatEncrypted = chat.IsChatEncrypted;
            chatToBeUpdated.IsChatEdited = chat.IsChatEdited;
            chatToBeUpdated.Name = chat.Name;
            context.Update(chatToBeUpdated);
            await context.SaveChangesAsync();
            return Ok(chatToBeUpdated);
        }
        
        [HttpPost("createchat")] //posts chat
        public async Task<ActionResult> CreateChat(ChatModel chat)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var checkIfExists = await context.Chats.FirstOrDefaultAsync(c => c.Name.ToUpper() == chat.Name.ToUpper());
            if (checkIfExists != null)
                return Conflict(new {message = $"An existing record with the name '{chat.Name}' already exists"});
            
            ChatModel chatToBeCreated = new()
            {
                Name = chat.Name,
                CreatorId = chat.CreatorId,
                IsChatEncrypted = chat.IsChatEncrypted,
                ChatCreated = DateTime.Now,
                Users = chat.Users,
                Messages = new List<MessageModel>()
                {
                    new()
                    {
                        Message = "",
                        MessageCreated = chat.ChatCreated,
                        ChatId = chat.Id,
                        UserId = chat.CreatorId
                    }
                },
            };
            await context.Chats.AddAsync(chatToBeCreated);
            await context.SaveChangesAsync();
            return Ok();
        }



        /*[HttpPost("createchat")]
        public async Task<ActionResult> CreateChat(ChatModel chat)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            ChatModel chatToBeCreated = new()
            {
                Name = chat.Name,
                CreatorId = chat.CreatorId,
                IsChatEdited = chat.IsChatEdited,
                IsChatEnded = chat.IsChatEnded,
                IsChatEncrypted = chat.IsChatEncrypted,
                ChatCreated = chat.ChatCreated,
                ChatEnded = chat.ChatEnded,
                Users = chat.Users,
                Messages = new List<MessageModel>()
                {
                    new()
                    {
                        Message = "Chat was created!",
                        MessageCreated = chat.ChatCreated
                    }
                },

            };
            /*var userInclude = await context.Users.Include(c => c.Chats)
                .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            userInclude.Chats.Add(chatToBeCreated);#1#
            
            await context.Chats.AddAsync(chatToBeCreated);
            await context.SaveChangesAsync();
            return Ok();
        }*/
    }
}