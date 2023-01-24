using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace N_Chat.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]


    public class MessageController : ControllerBase
    {
        private readonly DataContext context;
        public MessageController(DataContext context)
        {
            this.context = context;
        }
        //GET:hämta en användares alla användar meddelanden
        [HttpGet("getUserMessages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllUserMessages(string userId)
        {
            var currentUser=await context.Users.FirstOrDefaultAsync(x=>x.Id==userId);
            if (currentUser == null)
            {
                return NotFound();
            }
            else
            {
                var userMessages = await context.Messages.ToListAsync();
                return Ok(userMessages);
            }
           
        }

        //GET:Hämta det nyaste meddelandet från en användares.
        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageModel>> GetMostRecentUserMessage(string userId,int messageId)
        {
            var currentUser= await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
           if(currentUser == null)
            {
                return NotFound();
            }
            else
            {
                var userMessage = await context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
                return userMessage;
            }
        }


        //POST: posta ett användar meddelande
        [HttpPost("postUserMessage")]
        public async Task<ActionResult<MessageModel>> PostUserMessage(string userid,MessageModel message)

        {
            //om det inte finns en user i databsen
            var currentUser=await context.Users.FirstOrDefaultAsync(x => x.Id == userid);   
            if (currentUser == null)
            {
                return BadRequest();
            }
            //annars skapa ett nytt meddelande till user
            else
            {
                var messageToAddToChat = new MessageModel()
                {
                    Message = message.Message,
                    User=currentUser,
                    UserId=userid,
                    MessageCreated = DateTime.Now,
                    IsMessageDeleted = false,
                    IsMessageEdited = false,
                    IsMessageEncrypted = false,
                };
                context.Messages.Add(messageToAddToChat);
                await context.SaveChangesAsync();
                return messageToAddToChat;
            }
           
        }

        //PUT:uppdatera ett användar meddelande
        [HttpPut("updateUserMessage")]
        public async Task<ActionResult> PutUserMessage(string userId,int messageId, MessageModel message)
        {
            var currentUser= await context.Users.FirstOrDefaultAsync(x => x.Id == userId);   
            if(currentUser == null)
            {
                return NotFound();
            }
            else
            {
                var userMessage = await context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
                if (messageId != message.Id)
                {
                    return BadRequest();
                }
                else
                {
                    context.Update(message);
                    await context.SaveChangesAsync();
                    return Ok(message);
                }
            }
            
           
        }

        //DELETE: Ta bort en användares meddelande( ska inte tas bort från databasen (softdelete),id)
        [HttpDelete("deleteUserMessage")]
        public async Task<ActionResult<MessageModel>> DeleteUserMessage(string userId,int messageId)
        {
            var currentUser=await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (currentUser == null)
            {
                return NotFound();
            }
            else
            {
                var userMessage = await context.Messages.FindAsync(messageId);
                if (userMessage == null)
                {
                    return NotFound();
                }
                context.Messages.Remove(userMessage);
                await context.SaveChangesAsync();
                return userMessage;
            }
            
            
        }
    }


}
