using Microsoft.EntityFrameworkCore;
using N_Chat.Server.Data;
using N_Chat.Shared;
using N_Chat.Server.Controllers;

namespace Test;

//test methods in class MessageController
public class TestMc{
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

    MessageModel _constructorMessageModel = new MessageModel();


    [TearDown]
    public void TearDown(){
        _context.Dispose();
    }

    [Test]
    public void Test_GetAllMessages(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context);

        //testa metoden i MessageController.cs
        var actual = messageController.GetAllMessages();

        //testa att objektet är av typ MessageModel
        Assert.IsInstanceOf<MessageModel>(actual);

        //testa alla meddelanden hämtats från table Messages.
        /*se hur många meddelanden som finns i table Messages.*/
        var expected = _context.Messages.Count();

        //if both have the same number of messages, the test is passed.
        Assert.That(actual.Count() == (expected), Is.True);
    }


    //GetMessage(int id)
    //hämta meddelandet med hjälp av id
    [Test]
    public void Test_GetMessage(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context);

        //hämta ett meddelande från table Messages, så jag får ett id som finns.
        var expect = _context.Messages.FirstOrDefault();
        var userId = expect.UserId;

        //testa metoden i MessageController.cs
        var actual = messageController.GetMessage(userId);

        //testa att objektet är av typ MessageModel
        Assert.IsInstanceOf<MessageModel>(actual);

        //if both objects have the same id, the test is passed.
        Assert.That(actual.UserID, Is.EqualTo(expect.UserId));
    }

    //test PostMessage(MessageModel messageModel)
    [Test]
    public void Test_PostMessage(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context);

        //skapa ett meddelande
        var message = new MessageModel(){
            UserId = "78",
            Message = "TestMc testar PostMessage",
            MessageCreated = DateTime.Now,
            IsMessageDeleted = false
        };

        //testa metoden i MessageController.cs
        var actual = messageController.PostMessage(message);

        //testa att meddelandet har lagts till i table Messages.
        var expected = _context.Messages.Where(x => x.UserId == message.UserId).FirstOrDefault();
        Assert.That(expected, Is.Not.Null);

        //if both objects have the same id, the test is passed.
        Assert.That(actual.UserID, Is.EqualTo(message.UserId));
    }

    //test PutMessage(MessageModel messageModel)
    //soft delete message
    [Test]
    public void Test_PutMessageSoftDelete(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context);

        //hämta ett meddelande från table Messages, så jag får ett id som finns.
        var message = _context.Messages.FirstOrDefault();
        var userId = message.UserId;

        //sätt IsMessageDeleted till true och MessageDeleted till DateTime.Now
        message.IsMessageDeleted = true;
        message.MessageDeleted = DateTime.Now;

        //testa metoden i MessageController.cs
        messageController.PutMessage(message);

        //testa att meddelandet är soft-delete:ed i table Messages.
        var expected = _context.Messages.Where(x => x.UserId == userId).FirstOrDefault();
        Assert.That(expected.IsMessageDeleted, Is.True);

        //if both objects have the same id, the test is passed.
        Assert.That(message.UserID, Is.EqualTo(expect.UserId));
    }

    //test PutMessage(MessageModel messageModel)
    //uppdaterar meddelande
    [Test]
    public void Test_PutMessageUpdate(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context);

        //hämta ett meddelande från table Messages.
        var original = _context.Messages.FirstOrDefault();
        var expected = original;

        //ändra meddelandet
        expected.Message = "TestMc kör testet Test_PutMessageUpdate som testar PutMessage i MessageController.cs";

        //testa metoden i MessageController.cs
        messageController.PutMessage(expected);

        //testa att meddelandet har uppdaterats i table Messages.
        var actual = _context.Messages.Where(x => x.UserId == expected.UserId).FirstOrDefault();

        //jämför gammalt meddelande med det nya
        Assert.That(original, Is.Not.SameAs(actual));

        //check if both objects have the same id
        Assert.That(original.UserID, Is.EqualTo(actual.UserId));
    }
}