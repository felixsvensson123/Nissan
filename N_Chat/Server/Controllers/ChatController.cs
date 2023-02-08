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

        [HttpGet("getall")]
        public async Task<IEnumerable<ChatModel>> GetAll()
        {
            List<ChatModel> chatList = await context.Chats.OfType<ChatModel>().Where(c => c.IsChatEnded != true).ToListAsync();
            return chatList;
        }

        [HttpGet("getchatbyid/{id}")]
        public async Task<ChatModel> GetById(int id)
        {
            var result = await GetAll(); 
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

        [HttpPost("createchat")]
        public async Task<ActionResult> CreateChat(ChatModel chat)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            ChatModel chatToBeCreated = new ()
                {
                    Name = chat.Name,
                    CreatorId = chat.CreatorId,
                    IsChatEdited = chat.IsChatEdited,
                    IsChatEnded = chat.IsChatEnded,
                    IsChatEncrypted = chat.IsChatEncrypted,
                    ChatCreated= chat.ChatCreated,
                    ChatEnded = chat.ChatEnded,
                    Messages = new List<MessageModel>()
                    {
                        new()
                        {
                            Message = "Chat was created!",
                            MessageCreated = chat.ChatCreated
                        }
                    },
  
                };
            var userInclude = await context.Users.Include(c => c.Chats)
                .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            userInclude.Chats.Add(chatToBeCreated);
            //context.Chats.Attach(chatToBeCreated).Entity.User.Chats.Add(chatToBeCreated);
            // user.Chats.Add(chatToBeCreated);
              await context.SaveChangesAsync(); 
              
              return Ok();
        }
    }
}