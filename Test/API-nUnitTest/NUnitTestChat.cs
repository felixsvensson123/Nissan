using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using N_Chat.Client.Services;
using N_Chat.Server.Controllers;
using N_Chat.Server.Data;
using N_Chat.Shared;
using NUnit.Framework;




namespace Test.API_nUnitTest
{
    public class NUnitTestChat
    {


        [TestFixture]
        public class ChatTestsAll
        {
            public HttpClient _httpClient;
            public DataContext _context;
            public IChatService _service;

            [SetUp]
            public void Setup()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://localhost:7280/api/chat/getall");
                _service = new ChatService(_httpClient);

                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "nchatdb")
                    .Options;
                _context = new DataContext(options);
                _context.Database.EnsureCreated();

                // Initialize database with test data
                _context.Chats.Add(new ChatModel { Name = "Chat 1", CreatorId = "", IsChatEdited = false });
                _context.Chats.Add(new ChatModel { Name = "Chat 2", CreatorId = "", IsChatEdited = false });
                _context.SaveChanges();
            }


            [TearDown]
            public void Teardown()
            {
                // Clean up database after test is complete
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }
            [Test]
            public void TestGetChatsFromDatabase()
            {
                // Retrieve chat data from database
                var chats = _context.Chats.ToList();
                Assert.Multiple(() =>
                {

                    // Assert that the retrieved data matches expectations
                    Assert.That(chats.Count(), Is.EqualTo(2));
                    Assert.That(chats.ElementAt(0).Name, Is.EqualTo("Chat 1"));
                    Assert.That(chats.ElementAt(1).Name, Is.EqualTo("Chat 2"));
                });

            }






        }




        [TestFixture]
        public class ChatTestsGetById
        {
            public HttpClient _httpClient;
            public DataContext _context;
            public IChatService _service;
            int id = 1;

            [SetUp]
            public void Setup()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://nissanchat.azurewebsites.net/api/getall");

                _service = new ChatService(_httpClient);

                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase(databaseName: "nchatdb")
                    .Options;
                _context = new DataContext(options);
                _context.Database.EnsureCreated();

                // Initialize database with test data/ find id 1


                _context.Chats.FirstOrDefault(x => x.Id == id);
                _context.SaveChanges();


            }
            [TearDown]
            public void Teardown()
            {
                // Clean up database after test is complete
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }


            [Test]
            public async Task TestGetAChatFromDatabase()
            {
                // Arrange
                int id = 1;


                // Act
                var chat = await _service.GetChatById(id);
                Console.WriteLine(chat);

                // Assert
                Assert.That(chat, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(chat.Id, Is.EqualTo(id));

                    Assert.That(_httpClient.BaseAddress + $"chat/getbyid/{id}", // Update URL to include correct endpoint
                        Is.EqualTo("https://localhost:7280/api/chat/getbyid/1"));

                });
            }


        }















    }
    
}

