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
                var user = await _context.Messages
                    .Where(u => u.UserId == id)
                    .Select(m=> m.IsMessageEncrypted 
                    ? keyVaultService.DecryptStringAsync(m.Message).Result 
                    : m.Message
                    )
                    .ToListAsync();
                
                return Ok(user);
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
                   userMessage.Message = await keyVaultService.DecryptStringAsync(userMessage.Message); 
             
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
                    messageModel.Message = await keyVaultService.EncryptStringAsync(messageModel.Message);

                var chat = await _context.Chats.FirstOrDefaultAsync(x => x.Id == messageModel.ChatId);
                chat.Messages.Add(new()
                    {
                        UserId = currentUser.Id,
                        IsMessageEncrypted = messageModel.IsMessageEncrypted,
                        IsMessageDeleted = messageModel.IsMessageDeleted,
                        ChatId = messageModel.ChatId,
                        MessageCreated = DateTime.Now,
                        Message = messageModel.Message,
                        UserName = messageModel.UserName
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
        public async Task<ActionResult> PutMessage(MessageModel messageModel, int id)
        {
            // välja meddelande med rätt messageId sen editera message. Även kolla så user inte raderat meddelande (pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för user:n).

            var updateUserMessage = await _context.Messages // find message where id is same as received id and then selects it
                .Where(p => p.Id == id)
                .Select(_new => new MessageModel
                {
                    Id = _new.Id,
                    Message = _new.IsMessageEncrypted
                        ? keyVaultService.EncryptStringAsync(messageModel.Message).Result // encrypts if bool is true
                        : messageModel.Message, // sets message to a non encrypted text if bool is false
                    IsMessageEncrypted = _new.IsMessageEncrypted,
                    IsMessageEdited = true,
                    IsMessageDeleted = messageModel.IsMessageDeleted,
                    MessageCreated = _new.MessageCreated,
                    MessageEdited = DateTime.Now,
                    MessageDeleted = messageModel.IsMessageDeleted
                        ? DateTime.Now // sets date time if IsMessageDeleted is true
                        : _new.MessageDeleted, // doesnt change it if IsMessageDeleted is false
                }).FirstOrDefaultAsync(x => x.Id == id);

            //hantera meddelandet fanns inte.
            if (updateUserMessage.Message == null)
            {
                return NotFound();
            }

            //uppdatera databasen.
            _context.Messages.Update(updateUserMessage);
            await _context.SaveChangesAsync();
            return Ok(updateUserMessage);
        }

        [HttpGet("getallmessages")]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetAllMessages(){
            try{
                var allMessages = await _context.Messages
                    .Select(m => new MessageModel
                    {
                        Id = m.Id,
                        Message = m.IsMessageEncrypted 
                            ? keyVaultService.DecryptStringAsync(m.Message).Result // decrypts if bool is true
                            : m.Message, // doesnt do anything if bool is false
                        ChatId = m.ChatId,
                        UserId = m.UserId,
                        UserName = m.UserName,
                        IsMessageEncrypted = m.IsMessageEncrypted,
                        IsMessageEdited = m.IsMessageEdited,
                        IsMessageDeleted = m.IsMessageDeleted,
                        MessageCreated = m.MessageCreated,
                        MessageEdited = m.MessageEdited,
                        MessageDeleted = m.MessageDeleted,
                        User = m.User
                    })
                    .ToListAsync();
                
                foreach (var item in allMessages)
                {
                    if(item.IsMessageEncrypted) // decrypts message using azure key vault service if statement is true
                        item.Message = await keyVaultService.DecryptStringAsync(item.Message); 
                }
                
                return Ok(allMessages);
            }
            catch (Exception e){
                return NotFound(e.Message + e.StackTrace);
            }
        }

        [HttpGet("GetChatMessages/{chatId}")]
        public async Task<ICollection<MessageModel>> GetChatMessages(int chatId)
        {
            var messages = await _context.Messages
                .Where(m => m.ChatId == chatId)
                .Select(m => new MessageModel
                {
                    Id = m.Id,
                    Message = m.IsMessageEncrypted 
                        ? keyVaultService.DecryptStringAsync(m.Message).Result 
                        : m.Message,
                    UserName = m.UserName,
                    ChatId = m.ChatId,
                    UserId = m.UserId,
                    IsMessageEncrypted = m.IsMessageEncrypted,
                    IsMessageEdited = m.IsMessageEdited,
                    IsMessageDeleted = m.IsMessageDeleted,
                    MessageCreated = m.MessageCreated,
                    MessageEdited = m.MessageEdited,
                    MessageDeleted = m.MessageDeleted,
                    User = m.User
                })
                .ToListAsync();
            
            return messages;
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
        /*                

                // välja meddelande med rätt messageId sen editera message. Även kolla så user inte raderat meddelande (pga betygkrav får inte meddelandet vara raderat från DB, men det är "soft-deleted" för user:n).

                //hitta meddelandet med id

                var updateUserMessage = await _context.Messages
                    .Select(_new => new MessageModel
                    {
                        Id = _new.Id,
                        Message = _new.IsMessageEncrypted ? keyVaultService.EncryptStringAsync(messageModel.Message).Result : messageModel.Message,
                        ChatId = _new.ChatId,
                        UserId = _new.UserId,
                        UserName = _new.UserName,
                        IsMessageEncrypted = _new.IsMessageEncrypted,
                        IsMessageEdited = true,
                        IsMessageDeleted = messageModel.IsMessageDeleted,
                        MessageCreated = _new.MessageCreated,
                        MessageEdited = DateTime.Now,
                        MessageDeleted = _new.IsMessageDeleted ? DateTime.Now: messageModel.MessageDeleted ,
                        User = _new.User
                    })
                    .ToListAsync();FirstOrDefaultAsync(p => p.Id ==id);

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
            */
    }
}