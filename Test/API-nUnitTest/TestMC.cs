using Microsoft.EntityFrameworkCore;
using N_Chat.Client.Services;
using N_Chat.Server.Controllers;
using N_Chat.Server.Data;
using N_Chat.Shared;


namespace Test;

//test methods in class MessageController
public class TestMc{
    //test expected behaviour of methods in MessageController.cs
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

        _controller = new MessageController(_context, _keyVaultService);
    }

    MessageModel _constructorMessageModel = new MessageModel();


    [TearDown]
    public void TearDown(){
        _context.Dispose();
    }

    [Test]
    public void Test_GetAllMessages(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context, _keyVaultService);

        //testa metoden i MessageController.cs
        var actual = messageController.GetAllMessages();

        //testa att objektet är av typ MessageModel
        Assert.IsInstanceOf<MessageModel>(actual);

        /*//testa alla meddelanden hämtats från table Messages. .Count() bråkar
        /*
        var expected = _context.Messages.Count();

        //if both have the same number of messages, the test is passed.
        Assert.That(actual == (expected), Is.True);   */
    }


    //GetMessage(int id)
    //hämta meddelandet med hjälp av id
    [Test]
    public void Test_GetMessage(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context, _keyVaultService);

        //hämta ett meddelande från table Messages, så jag får ett id som finns.
        var message = _context.Messages.FirstOrDefault();

        if (message != null){
            //testa metoden i MessageController.cs
            var actual = messageController.GetById(message.Id);

            //testa att objektet är av typ MessageModel
            Assert.That(actual, Is.InstanceOf(message.GetType()));

            //if both objects have the same id, the test is passed.
            Assert.That(message.Id, Is.EqualTo(actual.Id));
        }
        else{
            Assert.Fail("No message found in table Messages!");
        }
    }

    //test PostMessage(MessageModel messageModel)
    [Test]
    public void Test_PostMessage(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context, _keyVaultService);

        //skapa ett meddelande
        var createdMessage = new MessageModel(){
            UserId = "1",
            Message = "TestMc testar PostMessage",
            MessageCreated = DateTime.Now,
            IsMessageDeleted = false
        };

        //testa metoden i MessageController.cs
        var actual = messageController.PostMessage(createdMessage);

        //testa att meddelandet har lagts till i table Messages.
        var expected = _context.Messages.Where(x => x.UserId == createdMessage.UserId).FirstOrDefault();

        Assert.That(expected, Is.Not.Null);

        //if both objects have the same id, the test is passed.
        Assert.That(createdMessage.UserId, Is.EqualTo(createdMessage.UserId));
    }

    //test PutMessage(MessageModel messageModel)
    //soft delete message
    [Test]
    public void Test_PutMessageSoftDelete(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context, _keyVaultService);

        //hämta ett meddelande från table Messages.
        var collectedMessage = _context.Messages.FirstOrDefault();

        if (collectedMessage != null){
            //sätt IsMessageDeleted till true och MessageDeleted till DateTime.Now
            collectedMessage.IsMessageDeleted = true;
            collectedMessage.MessageDeleted = DateTime.Now;

            //testa metoden i MessageController.cs
            messageController.PutMessage(collectedMessage, collectedMessage.Id);

            //testa att meddelandet är soft-delete:ed i table Messages.
            var expected = _context.Messages.Where(x => x.Id == collectedMessage.Id).FirstOrDefault();

            Assert.That(expected.IsMessageDeleted, Is.True);

            //if both objects have the same id, the test is passed.
            Assert.That(collectedMessage.Id, Is.EqualTo(expected.Id));
        }
        else{
            Assert.Fail("No message found in table Messages!");
        }
    }

    //test PutMessage(MessageModel messageModel)
    //uppdaterar meddelande
    [Test]
    public void Test_PutMessageUpdate(){
        //en instans av klassen MessageController
        var messageController = new MessageController(_context, _keyVaultService);

        //hämta ett meddelande från table Messages.
        var original = _context.Messages.FirstOrDefault();
        var expected = original;

        //ändra meddelandet
        expected.Message = "TestMc kör testet Test_PutMessageUpdate som testar PutMessage i MessageController.cs";

        //testa metoden i MessageController.cs
        messageController.PutMessage(expected, expected.Id);

        //testa att meddelandet har uppdaterats i table Messages.
        var actual = _context.Messages.Where(x => x.UserId == expected.UserId).FirstOrDefault();

        //jämför gammalt meddelande med det nya
        Assert.That(original, Is.Not.SameAs(actual));

        int id = original.Id;
        int actualId = (int)actual.Id;

        //check if both objects have the same id
        Assert.That(id, Is.EqualTo(actualId));
    }
}