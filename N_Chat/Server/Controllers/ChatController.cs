using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            ChatModel chatToBeCreated = new ChatModel();
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            chatToBeCreated.Name = chat.Name;
            chatToBeCreated.Id = chat.Id;
            chatToBeCreated.CreatorId = chat.CreatorId;
            chatToBeCreated.IsChatEdited = chat.IsChatEdited;
            chatToBeCreated.IsChatEnded = chat.IsChatEnded;
            chatToBeCreated.IsChatEncrypted = chat.IsChatEncrypted;
            chatToBeCreated.ChatCreated = chat.ChatCreated;
            chatToBeCreated.ChatEnded = chat.ChatEnded;
            chatToBeCreated.Messages = chat.Messages;
            chatToBeCreated.Users = chat.Users;
            chatToBeCreated.UserId = chat.UserId;
            context.Add(chatToBeCreated);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}