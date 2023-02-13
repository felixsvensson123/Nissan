using Microsoft.AspNetCore.Mvc;

namespace N_Chat.Server.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase{
        private readonly DataContext _context;
        private readonly IKeyVaultService keyVaultService;
        public MessageController(DataContext context, IKeyVaultService keyVaultService){
            _context = context;
            this.keyVaultService = keyVaultService;
        }

        //GET:hämta en användares alla chatt meddelanden
        /* IsMessageDeleted används för att kolla om message är softdeleted (och om det är true visas meddelandet inte för användaren pga det är softdeleted).*/
        [HttpGet("usermessages/{id}")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllUserMessages(string id){
            try{
                var userMessages = await _context.Messages
                  .Where(u => u.UserId == id && u.IsMessageDeleted != true)
                  .ToListAsync();
                
                foreach (var item in userMessages)
                {
                    if(item.IsMessageEncrypted) // decrypts message using azure key vault service if statement is true
                        await keyVaultService.DecryptStringAsync(item.Message); 
                }
                
                return userMessages;
            }
            catch (Exception e){
                return NotFound(e.Message + e.StackTrace);
            }
        }

        //GET:Hämta det nyaste meddelandet från en användare.
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageModel>> GetById(int id){
            try{
                //hämtar meddelande med meddelande id
               var userMessage = await _context.Messages.FirstOrDefaultAsync(p => p.Id == id);
               
               if(userMessage.IsMessageEncrypted) // decrypts message using azure key vault service if statement is true
                   await keyVaultService.DecryptStringAsync(userMessage.Message); 
             
               return Ok(userMessage);
            }
            catch (Exception e){
                return NotFound(e.Message + e.StackTrace);
            }
        }


        //POST: posta ett chattmeddelande
        [HttpPost("postmessage")]
        public async Task<ActionResult<MessageModel>> PostMessage(MessageModel messageModel){
            try{
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == messageModel.UserId);
                if (currentUser == null){
                    return NotFound();
                }
                
                if(messageModel.IsMessageEncrypted) // encrypts message using azure key vault service if statement is true
                    await keyVaultService.EncryptStringAsync(messageModel.Message);

                var chat = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageModel.ChatId);
                chat.Messages.Add(new()
                    {
                        UserId = currentUser.Id,
                        IsMessageEncrypted = messageModel.IsMessageEncrypted,
                        IsMessageDeleted = messageModel.IsMessageDeleted,
                        ChatId = messageModel.ChatId,
                        MessageCreated = DateTime.Now,
                        Message = messageModel.Message
                    }
                );
                //skapar ett nytt meddelande till databasen
                _context.Update(chat);
                await _context.SaveChangesAsync();
               
                return Ok();
            }
            catch (Exception e){
                return NotFound(e.Message + e.StackTrace);
            }
        }

        //PUT:uppdatera ett chattmeddelande
        [HttpPut("updatemessage/{id}")]
        public async Task<ActionResult> PutMessage(MessageModel messageModel,int id){
            try{
               

                // välja meddelande med rätt messageId sen editera message. Även kolla så user inte raderat meddelande (pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för user:n).

                //hitta meddelandet med id

                var updateUserMessage = await _context.Messages
                    .Where(x => x.UserId == messageModel.UserId).FirstOrDefaultAsync(p => p.Id ==id);

                //hantera meddelandet fanns inte.
                if (updateUserMessage == null){
                    return BadRequest();
                }
                //uppdatera meddelandet.
                updateUserMessage.IsMessageEncrypted = messageModel.IsMessageEncrypted;
                updateUserMessage.IsMessageDeleted = messageModel.IsMessageDeleted;              
                updateUserMessage.ChatId = messageModel.ChatId;         
                updateUserMessage.Message = messageModel.Message;
                updateUserMessage.MessageDeleted = messageModel.MessageDeleted;

                //uppdatera meddelandet i databasen.
                _context.Update(updateUserMessage);
                await _context.SaveChangesAsync();
                return Ok(updateUserMessage);
            }
            catch (Exception e){
                return NotFound(e.Message + e.StackTrace);
            }
        }

        [HttpGet("getallmessages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllMessages(){
            try{
                var allMessages = await _context.Messages.ToListAsync();
                
                foreach (var item in allMessages)
                {
                    if(item.IsMessageEncrypted) // decrypts message using azure key vault service if statement is true
                        await keyVaultService.DecryptStringAsync(item.Message); 
                }
                
                return Ok(allMessages);
            }
            catch (Exception e){
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