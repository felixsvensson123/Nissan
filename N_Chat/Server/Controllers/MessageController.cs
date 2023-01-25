using Microsoft.AspNetCore.Mvc;

namespace N_Chat.Server.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase{
        private readonly DataContext _context;

        public MessageController(DataContext context){
            _context = context;
        }

        //GET:hämta en användares alla chatt meddelanden
        /* IsMessageDeleted används för att kolla om message är softdeleted (och om det är true visas meddelandet inte för användaren pga det är softdeleted).*/
        [HttpGet("GetAllUserMessages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllUserMessages(MessageModel messageModel){
            try{
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);

                var userMessages = await _context.Messages
                    .Where(u => u.UserId == messageModel.UserId && u.IsMessageDeleted != false).ToListAsync();
                return Ok(userMessages);
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return NotFound();
            }
        }

        //GET:Hämta det nyaste meddelandet från en användare.
        [HttpGet("{messageId}")]
        public async Task<ActionResult<MessageModel>> GetMostRecentMessage(MessageModel messageModel){
            try{
                //select using user.id + OrderByDescending() & MessageCreated and that is not soft-deleted.

                var userMessage = await _context.Messages
                    .Where(x => x.UserId == messageModel.UserId && x.IsMessageDeleted != false)
                    .OrderByDescending(y => y.MessageCreated).FirstOrDefaultAsync();

                return userMessage;
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return NotFound();
            }
        }


        //POST: posta ett chattmeddelande
        [HttpPost("postUserMessage")]
        public async Task<ActionResult<MessageModel>> PostUserMessage(string userId, MessageModel message){
            //om det inte finns en user i databasen
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (currentUser == null){
                return NotFound();
            }
            //spara ett nytt meddelande till DB
            else{
                var messageToAddToChat = new MessageModel(){
                    Message = message.Message,
                    User = currentUser,
                    UserId = userId,
                    MessageCreated = DateTime.Now,
                    IsMessageDeleted = false,
                    IsMessageEdited = false,
                    IsMessageEncrypted = false,
                };
                _context.Messages.Add(messageToAddToChat);
                await _context.SaveChangesAsync();
                return messageToAddToChat;
            }
        }

        //PUT:uppdatera ett chattmeddelande
        [HttpPut("updateMessage")]
        public async Task<ActionResult> PutUserMessage(string userId, int messageId, string editedMessage){
            //check if user exist.
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (currentUser == null){
                return NotFound();
            }

            // välja meddelande med rätt messageId sen editera message. Även kolla så user inte raderat meddelande (pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för user:n).
            
            //hitta meddelandet med id.
            var updatedMessage = await _context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);

            //hantera meddelandet fanns inte.
            if (updatedMessage == null){
                return BadRequest();
            }

            //uppdatera chattmeddelandet, ersätta det gamla med det nya.
            updatedMessage.Message = editedMessage;

            //uppdatera meddelandet i databasen där meddelandeId = messageId.
            _context.Update(updatedMessage);
            await _context.SaveChangesAsync();
            return Ok(updatedMessage);
        }

        //SOFTDELETE: soft-delete ett chatt meddelande 
        //pga betygkrav fpr inte meddelandet vara raderat från DB, pga meddelandet är "soft-deleted" kan vi läsa av boolean IsMessageDeleted och/eller MessageDeleted.
        public bool SoftDeleteUserMessage(int messageId){
            var userMessage = _context.Messages.Find(messageId);
            if (userMessage == null){
                return false;
            }

            userMessage.MessageDeleted = DateTime.Now; //time chat message was deleted. 
            userMessage.IsMessageDeleted = true; // är true om message är softdeleted 
            
            //Saves all changes made in this context to the database.
            _context.SaveChanges();
            return true;
        }
    }
}