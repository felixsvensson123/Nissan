using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N_Chat.Server.Data;
using N_Chat.Server.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class _
    {
        [TestFixture]
        public class MessageControllerTests
        {
            private DataContext _context;
            private MessageController _controller;

            [SetUp]

            public void Setup()
            {
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "MessageTest")
                    .Options;
                _context = new DataContext(options);
                _controller = new MessageController(_context);
            }

            [TearDown]
            public void TearDown()
            {
                _context.Dispose();
                _controller = null;
            }

            [Test]
            public void Test_GetAllUserMessages_ReturnsOkResult()
            {
                //Arrange
                //Act
                var okResult = _controller.GetAllUserMessages();

                //Assert
                Assert.IsTrue(okResult.IsCompletedSuccessfully);
            }

            [Test]
            public void Test_GetMostRecentUserMessage_ReturnsOkResult()
            {
                //Arrange
                var messageId = 1;

                //Act
                var okResult = _controller.GetMostRecentUserMessage(messageId);

                //Assert
                Assert.IsTrue(okResult.IsCompleted);
                    }
        }

    }
}
