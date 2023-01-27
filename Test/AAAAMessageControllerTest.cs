using Microsoft.EntityFrameworkCore;
using N_Chat.Server.Data;
using N_Chat.Shared;
using System.Data.Entity;
using N_Chat.Server.Controllers;

//import MessageController.cs ? 

namespace Test{
    public class AaaaMessageControllerTest{
        //test expected behaviour of methods in MessageController.cs
        private DataContext _context;
        private MessageController _controller;

        [SetUp]
        public void Setup(){
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UserMessageTest")
                .Options;
            _context = new DataContext(options);
            _controller = new MessageController(_context);
        }

        MessageModel constructorMessageModel = new MessageModel();

        [TearDown]
        public void TearDown(){
            _context.Dispose();
        }

        [Test]
        public void TestGetAllUserMessages(){
            try{
                //find a message that we know exist, so we get a userId that exists in the Message table. 
                var messageObject = _context.Messages.FirstOrDefaultAsync();

                //use the method in MessageController.cs to get all messages from the user.

                MessageModel messageModel = new MessageModel();

                var userMessages = MessageController.GetAllUserMessages(messageModel);
            }
            catch (Exception e){
                Console.WriteLine("ERROR!! " + e.StackTrace + " " + e.Message);
            }
        }

        //test GetMostRecentMessage()

        //test PostUserMessage()

        //test DeleteUserMessage()

        //test UpdateUserMessage()

        //test SoftDeleteUserMessage()
    }
}