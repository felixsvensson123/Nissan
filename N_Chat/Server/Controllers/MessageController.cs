using Microsoft.AspNetCore.Mvc;

namespace N_Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _context;

        public MessageController(DataContext context)
        {
            _context = context;
        }

        //GET:hämta en användares alla chatt meddelanden
        /* IsMessageDeleted används för att kolla om message är softdeleted (och om det är true visas meddelandet inte för användaren pga det är softdeleted).*/
        [HttpGet("GetAllUserMessages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllUserMessages(MessageModel messageModel)
        {
            try
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);
                if (currentUser == null)
                {
                    return NotFound();
                }

                var userMessages = await _context.Messages
                  .Where(u => u.UserId == messageModel.UserId && u.IsMessageDeleted != true).ToListAsync();
                return Ok(userMessages);
            }
            catch (Exception e)
            {

                return NotFound(e.Message + e.StackTrace);
            }
        }
        //GET:Hämta det nyaste meddelandet från en användare.
        [HttpGet("{GetMessageId}")]
        public async Task<ActionResult<MessageModel>> GetMostRecentMessage(MessageModel messageModel)
        {
            try
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);
                if (currentUser == null)
                {
                    return NotFound();
                }

                //select using user.id + OrderByDescending() & MessageCreated and that is not soft-deleted.

                var userMessage = await _context.Messages.FirstOrDefaultAsync(p => p.Id == messageModel.Id);

                //.Where(x => x.UserId == messageModel.UserId && x.IsMessageDeleted != true).OrderByDescending(y => y.MessageCreated).
                return Ok(userMessage);
            }
            catch (Exception e)
            {
                return NotFound(e.Message + e.StackTrace);
            }
        }


        //POST: posta ett chattmeddelande
        [HttpPost("postNewUserMessage")]
        public async Task<ActionResult<MessageModel>> PostUserMessage(MessageModel messageModel)
        {
            try
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);
                if (currentUser == null)
                {
                    return NotFound();
                }

                //skapar ett nytt meddelande till databasen
                await _context.AddAsync(new MessageModel()
                {
                    UserId = currentUser.Id,
                    Id = messageModel.Id,
                    IsMessageEncrypted = messageModel.IsMessageEncrypted,
                    IsMessageDeleted = messageModel.IsMessageDeleted,
                    ChatId = messageModel.ChatId,
                    MessageCreated = DateTime.Now,
                    Message = messageModel.Message,
                });
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {

                return NotFound(e.Message + e.StackTrace);

            }


        }

        //PUT:uppdatera ett chattmeddelande
        [HttpPut("updateMessage")]
        public async Task<ActionResult> PutUserMessage(MessageModel messageModel)
        {

            try
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);
                if (currentUser == null)
                {
                    return NotFound();
                }


                // välja meddelande med rätt messageId sen editera message. Även kolla så user inte raderat meddelande (pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för user:n).

                //hitta meddelandet med id

                var updateUserMessage = await _context.Messages
                   .Where(x => x.UserId == messageModel.UserId).FirstOrDefaultAsync(p => p.Id == messageModel.Id);

                //hantera meddelandet fanns inte.
                if (updateUserMessage == null)
                {

                    return BadRequest();
                }
                //uppdatera meddelandet i databasen där meddelandeId = messageId.

                updateUserMessage.IsMessageEncrypted = messageModel.IsMessageEncrypted;
                updateUserMessage.IsMessageDeleted = messageModel.IsMessageDeleted;
                updateUserMessage.MessageCreated = messageModel.MessageCreated;
                updateUserMessage.ChatId = messageModel.ChatId;
                updateUserMessage.MessageCreated = DateTime.Now;
                updateUserMessage.Message = messageModel.Message;
                updateUserMessage.MessageDeleted = messageModel.MessageDeleted;


                _context.Update(updateUserMessage);
                await _context.SaveChangesAsync();
                return Ok(updateUserMessage);

            }
            catch (Exception e)
            {

                return NotFound(e.Message + e.StackTrace);

            }
        }
        [HttpGet("getallmessages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllMessages(MessageModel messageModel)
        {
            try
            {


                var allMessages = await _context.Messages.ToListAsync();
                return Ok(allMessages);
            }
            catch (Exception e)
            {

                return NotFound(e.Message + e.StackTrace);
            }
        }

        //SOFTDELETE: soft-delete ett chatt meddelande 
        //pga betygkrav fpr inte meddelandet vara raderat från DB, pga meddelandet är "soft-deleted" kan vi läsa av boolean IsMessageDeleted och/eller MessageDeleted.
        /* [HttpPut("SoftDeleteUserMessage")]
         public async Task SoftDeleteUserMessage(MessageModel messageModel)
         {
             try
             {
                 //hämtar användaren
                 var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);


                 //hämtar användarens meddelande
                 var messageToSoftDelete = await _context.Messages
                        .Where(x => x.UserId == messageModel.UserId && x.IsMessageDeleted == false)
                        .OrderByDescending(y => y.MessageCreated).FirstOrDefaultAsync(p => p.Id == messageModel.Id);



                 if (messageToSoftDelete != null)
                 {
                     messageToSoftDelete.MessageDeleted = DateTime.Now; //time chat message was deleted. 
                     messageToSoftDelete.IsMessageDeleted = true; // är true om message är softdeleted 

                     //Saves all changes made in this context to the database.
                     _context.Update(messageToSoftDelete);
                     await _context.SaveChangesAsync();



                 }

             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message);
                 Console.WriteLine(e.StackTrace);

             }*/



    }
}