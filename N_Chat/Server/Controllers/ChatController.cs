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

        
        /// <This is a C# method with an HTTP Post attribute. The method takes in 4 parameters: a string "creatorId", a string "chatName", a Boolean "encrypted", and a List of "UserModel" objects "joiners".
        /// The method creates a new "ChatModel" object and sets its properties such as Name, CreatorId, IsChatEdited, IsChatEnded, IsChatEncrypted, ChatCreated and Users. The new chat is then added to a database context and saved using the "SaveChangesAsync" method.
        /// Finally, the method returns the just created chat by finding it in the database context based on the CreatorId and ChatCreated properties. In case of an exception, the error message and stack trace is written to the console and the method returns "null".>
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="chatName"></param>
        /// <param name="encrypted"></param>
        /// <param name="joiners"></param>
        /// <returns></returns>
        //url namn för API metoden.
        [HttpPost("createchatuseinput/{creatorId}")]
        public async Task<string> CreateChatUseInput(string creatorId, ChatModel chatObject){

            try{
                
                // Implement a thread-safe pattern for accessing the DbContext instance, to prevent encountering a concurrency issue when saving to DB.
                // One such pattern is to create a new instance of DbContext for each operation, and dispose of it once the operation is complete. This way, each operation will have its own independent instance of DbContext and will not interfere with other operations.
                // Using the "using" statement in C# to ensure that the DbContext instance is properly disposed of, even in the event of an exception.

                /*
                using (var _context = new DataContext()){
                    
                }
                */
                
                
                
                ChatModel chat = chatObject; 
                context.Chats.Add(chat);
                await  context.SaveChangesAsync();

                  //below: was issues with treads.
                //get chat and return it because we need the ChatId that was assigned by DB to the chat.
                /*ChatModel getTheJustCreatedChat = await context.Chats.FirstOrDefaultAsync(u =>
                    u.CreatorId == creatorId && u.ChatCreated == chatObject.ChatCreated);*/
                var getTheJustCreatedChat =  context.Chats.OfType<ChatModel>().Where(u =>
                    u.CreatorId == creatorId && u.ChatCreated == chatObject.ChatCreated);
                
                string chatId = getTheJustCreatedChat.ToListAsync().Id.ToString();

                return chatId;
            }
            catch (Exception e){
                Console.WriteLine("Error trying to create chat.");
                Console.WriteLine(e.Message + ". Stacktrace:");
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        
        
        
        

        [HttpPost("createchat")]
        public async Task<ActionResult> CreateChat(ChatModel chatObject)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == chatObject.UserId);
            var user2 = await context.Users.FirstOrDefaultAsync(x => x.UserName == "admin");
            if (user != null)
            {
                ChatModel chatToBeCreated = new ()
                {
                    User = user,
                    UserId = chatObject.UserId,
                    Name = chatObject.Name,
                    CreatorId = chatObject.CreatorId,
                    IsChatEdited = chatObject.IsChatEdited,
                    IsChatEnded = chatObject.IsChatEnded,
                    IsChatEncrypted = chatObject.IsChatEncrypted,
                    ChatCreated= chatObject.ChatCreated,
                    ChatEnded = chatObject.ChatEnded,
                    Users = new List<UserModel>()
                    {
                        user,
                        user2
                    },
                    Messages = new List<MessageModel>()
                    {
                        new()
                        {
                            UserId = chatObject.UserId,
                            Message = chatObject.Name,
                            MessageCreated = chatObject.ChatCreated
                        }
                    },
  
                };
               context.Chats.Add(chatToBeCreated);
               user.Chats.Add(chatToBeCreated);
            }
            await context.SaveChangesAsync();
            return Ok();
        }
    }
    
    
    
}