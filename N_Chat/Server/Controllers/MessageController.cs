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
        //GET:hämta en användares alla chattmeddelanden
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

        //GET:Hämta det nyaste meddelandet från en användare.
        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageModel>> GetMostRecentMessage(string userId,int messageId)
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


        //POST: sparar ett chattmeddelande till DB
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

        //PUT:uppdatera ett chattmeddelande
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
                //första meddelandet vi hittar vars id stämmer med det id vi söker på && usern har inte raderat meddelandet (pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för usern).
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

        //DELETE: Ta bort ett chatt meddelande( ska inte tas bort från databasen (softdelete),id)
        //pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för usern här.
       
        public bool SoftDeleteUserMessage(int Id)
        {
               var userMessage = context.Messages.Find(Id);
                    if (userMessage == null)
                    {
                        return false;
                    }
                    else
                    {
                        userMessage.IsMessageDeleted=true;  
                        context.SaveChanges();
                        return true;

                   
                    }
                    
               
            


        }
    }


}
