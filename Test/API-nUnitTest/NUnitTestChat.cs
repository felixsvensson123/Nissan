using N_Chat.Client.Services;
using N_Chat.Shared;
using System.Net.Http;
using System.Threading.Tasks;




namespace Test.API_nUnitTest
{
    public class NUnitTestChat
    {
            [TestFixture]
            public class ChatTests
            {

                private HttpClient _httpClient;
                private ChatService _service;

            [SetUp]
            public void Setup()
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://localhost:7280/getall");
                _service = new ChatService(_httpClient);
            }




            [Test]//test to get all chats in db
                public async Task TestGetAllChats()
                {
                    // Act
                    int expectedChatCount = 2;
                    
                    //Arrange
                    List<ChatModel> chats = await _service.GetChats();


                    // Assert
                    Assert.Multiple(() =>
                    {
                        //chats is not empty
                        Assert.That(chats, Is.Not.Empty);                      
                        //there is chats in the list
                        Assert.That(chats.Any(), Is.True);
                        //contain 10 chat in the list
                        Assert.AreEqual(expectedChatCount, chats.Count);

                    });
                }
                [Test]//admin chat exist in chats db
                public async Task TestGetChatById()
                {
                    // Arrange
                    int chatId=1;
                    string creatorId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35";//admin id
                    string chatName = "EncryptChatTest";
                    bool ischatEdit = false;
                    bool ischatEnded = false;
                    bool ischatEncrypt = true;
                   
                   
                    ChatModel expectedChat = new() { Id = chatId,CreatorId = creatorId,IsChatEnded=ischatEnded,IsChatEncrypted=ischatEncrypt,IsChatEdited=ischatEdit,Name=chatName};

                    //Act
                    ChatModel Chatlist = await _service.GetChatById(chatId);

                    // Assert
                    Assert.AreEqual(expectedChat, Chatlist);
                }

                [Test]//felix create group chat-encrypt with calin and anna in chats db
                public async Task TestPostNewGroupChatEncryptedToList()
                {
                    //Arrange
                    
                    string chatName = "Felix-My new Encrypted Group chat";
                    bool encrypted = true;
                    string creatorid= "d7fc4ba6-4957-41a7-96b5-52b65c06bc35";
                    //UserChats userId1 = new() { UserId = "" };
                    //UserChats userId2 = new() { UserId = "" };
                    //List<UserModel> usersInNewChat = new() { userId1, userId2 };

                    //ChatModel expectedNewchat = new() { Name = chatName, IsChatEncrypted = encrypted, CreatorId =creatorid,Users=usersInNewChat };

                  

                    List<ChatModel> newChattoChatlist = await _service.GetChats();

                   
                    //Assert.That(newChattoChatlist, Does.Contain(expectedNewchat));

                }
            


                [Test]// felix create group chat-non encrypt with calin and anna in chats db
                public async Task TestCreateaNewGroupChatNotEncryptedToList()
                {
                string chatName = "Felix-My new Non Encrypt Group chat";
                bool encrypted = false;
                string creatorid = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35";

                //UserChats userId1 = new() { UserId = "" };
                //UserChats userId2 = new() { UserId = "" };
                //List<UserModel> usersInNewChat = new() { userId1, userId2 };

                //ChatModel expectedNewchat = new() { Name = chatName, IsChatEncrypted = encrypted, CreatorId = creatorid, Users = usersInNewChat };

               

                List<ChatModel> newChattoNotEncyptToChatlist = await _service.GetChats();

             
                //Assert.That(newChattoNotEncyptToChatlist, Does.Contain(expectedNewchat));

            }
                [Test]//a chat is softdeleted from the chat list in db.
                public async Task TestSoftDeleteAUserChatFromList()
                {
               

                    // Arrange
                    int chatId = 1;
                    bool isChatSoftDeleted = true;
                    ChatModel existingChat = await _service.GetChatById(chatId);//hämtar current chatId

                    // Act
                    ChatModel updatedChat = new() { Id =chatId,IsChatEnded=isChatSoftDeleted };//new updated chat-is deleted

                    _service.UpdateChat(updatedChat, chatId);

                     // Assert
                    ChatModel newChat = await _service.GetChatById(chatId);
                    Assert.AreEqual(updatedChat.IsChatEnded, newChat.IsChatEnded);


            }
                [Test]//
                public async Task TestUpdateAUserChatFromList()
                {
                    //Act
                    string userId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35";
                    int chatId = 1; 
                    string newChatName = "My New chat Name-Admin Special Chat";
                    bool isChatEdited = true;

                    //hämtar chatid som ska uppdateras
                    ChatModel updateChatName = await _service.GetChatById(chatId);

                    //Arrange

                    //
                    ChatModel actualChatWithnewName= new() {Name= newChatName,CreatorId=userId,Id=chatId,IsChatEdited =isChatEdited};
                    _service.UpdateChat(updateChatName, chatId);

                   ChatModel chatwithNewName= await _service.GetChatById(chatId);
                   Assert.AreEqual(updateChatName.IsChatEdited,chatwithNewName.IsChatEdited);
                    
                   

                 
                   //Assert

                }

                [Test]//felix create a singel chat-not encrypt with calin in chats db
                public async Task TestCreateaNewSingelChatNotEncryptedToList()
                {
                    string chatName = "Felix-My chat with Calin-NonEncrypt";
                    bool encrypted = false;
                    string creatorid = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35";

                    //UserChats userId = new() { UserId = "" };
                   
                   // List<UserChats> usersInNewChat = new() { userId};

                    //ChatModel expectedNewSingelchat = new() { Name = chatName, IsChatEncrypted = encrypted, CreatorId = creatorid, Users = usersInNewChat };

                    

                    List<ChatModel> newSingelChatToChatlist = await _service.GetChats();

                    
                   // Assert.That(newSingelChatToChatlist, Does.Contain(expectedNewSingelchat));

                }

                [Test]//felix create singel chat-encrypt with calin in chats db
                public async Task TestCreateNewSingelEncryptedChatToList()
                {
                string chatName = "Felix-My chat with Calin-Encrypt";
                bool encrypted = true;
                string creatorid = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35";

                //UserChats userId = new() { UserId = "" };

                //List<UserChats> usersInNewChat = new() { userId };// adding new usser to our chat

                //ChatModel expectedNewSingelchat = new() { Name = chatName, IsChatEncrypted = encrypted, CreatorId = creatorid, Users = usersInNewChat };// creating a new ccha¨t with properties

                //Arrange

                List<ChatModel> newSingelChatToChatlist = await _service.GetChats(); // fetching chatlist via service that connected to controller

                //Assert
                //Assert.That(newSingelChatToChatlist, Does.Contain(expectedNewSingelchat));// testing if the new chatexist in chatlist.
                }


            }



       

            


           
        
    }
}

