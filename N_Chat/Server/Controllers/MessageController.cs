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
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllUserMessages()
        {
            var messages = await context.Messages.OfType<MessageModel>().ToListAsync();
            return messages;
        }

        //GET:Hämta det nyaste meddelandet från en användares.
        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageModel>> GetMostRecentUserMessage(int messageId)
        {
            var message = await context.Messages.OrderByDescending(x => x.MessageCreated).FirstOrDefaultAsync(x => x.MessageId == messageId);
            return message;
        }


        //POST: posta ett användar meddelande
        [HttpPost("postUserMessage")]
        public async Task<ActionResult<MessageModel>> PostMessage(MessageModel message)
        {
            context.Messages.Add(message);
            await context.SaveChangesAsync();
            return message;
        }

        //PUT:uppdatera ett användar meddelande
        [HttpPut("updateUserMessage")]
        public async Task<ActionResult> PutMessage(int messageId, MessageModel message)
        {
            if (messageId != message.MessageId)
            {
                return BadRequest();
            }
            else
            {
                context.Entry(message).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(message);
            }
        }

        //DELETE: Ta bort en användares meddelande( ska inte tas bort från databasen (softdelete),id)
        [HttpDelete("deleteUserMessage")]
        public async Task<ActionResult<MessageModel>> DeleteMessage(int id)
        {
            var message = await context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
            return message;
        }
    }


}
