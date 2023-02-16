using N_Chat.Server.Data;
using N_Chat.Server.Controllers;
using Microsoft.EntityFrameworkCore;
using N_Chat.Client.Services;
using N_Chat.Shared;


namespace Test{
    public class _{
        [TestFixture]
        public class MessageControllerTests{
            private DataContext _context;
            private MessageController _controller;
            private IKeyVaultService _keyVaultService;

            [SetUp]
            public void Setup(){
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "UserMessageTest")
                    .Options;
                _context = new DataContext(options);
                
                _keyVaultService = this._keyVaultService;
               
                _controller = new MessageController(_context,_keyVaultService);
            }

            [TearDown]
            public void TearDown(){
                _context.Dispose();
                _controller = null;
            }


            [Test]
            public void Test_GetAllUserMessages_ReturnsOkResult(){
                //Arrange
                var user = _context.Users.FirstOrDefaultAsync();
                string userId = user.Id.ToString();

                MessageModel messageModel = new MessageModel();

                //Act
                var okResult = _controller.GetAllUserMessages(userId);

                //Assert
                Assert.IsTrue(okResult.IsCompletedSuccessfully);
            }

            [Test]
            public void Test_GetMostRecentUserMessage_ReturnsOkResult(){
                //Arrange
                //var messageId = 1;
                var user = _context.Users.FirstOrDefaultAsync();
                int id = user.Id;
                MessageModel messageModel = new MessageModel();

                //Act
                var okResult = _controller.GetById(id);

                //Assert
                Assert.IsTrue(okResult.IsCompleted);
            }

        }
    }
}