using N_Chat.Shared;
using System.Net.Http.Json;

namespace N_Chat.Client.Services{
    public class ChatService : IChatService{
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient){
            _httpClient = httpClient;
        }

        public async Task<List<ChatModel>> GetChats(){
            var result = await _httpClient.GetFromJsonAsync<List<ChatModel>>("api/chat/getall");
            return result;
        }

        public async Task<ChatModel> GetChatById(int id){
            var result = await _httpClient.GetFromJsonAsync<ChatModel>("api/chat/getbyid/{id}");
            return result;
        }

        public async Task<string> UpdateChat(ChatModel chat, int id){
            var result = await _httpClient.PutAsJsonAsync<ChatModel>("api/chat/updatechat/{id}", chat);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateChat(ChatModel chat){
            var result = await _httpClient.PostAsJsonAsync<ChatModel>("api/chat/createchat", chat);
            return await result.Content?.ReadAsStringAsync();
        }


        
        
        
        
        
        public async Task<string> CreateChatUseInput(string creatorId, string chatName, bool encrypted,
            List<UserModel> joiners){
            try{
                //vi vet cratorId funkar + joiners finns.
                
                //skapa chat
                ChatModel chatToBeCreated = new ChatModel();

                //populates chatToBeCreated.
                chatToBeCreated.Name = chatName + ""; //hantera om tomt chatname.
                chatToBeCreated.CreatorId = creatorId;
                chatToBeCreated.IsChatEdited = false;
                chatToBeCreated.IsChatEnded = false;
                chatToBeCreated.IsChatEncrypted = encrypted;
                chatToBeCreated.ChatCreated = DateTime.Now;
                chatToBeCreated.Users = joiners;
                
                var result = await _httpClient.PostAsJsonAsync<ChatModel>($"api/chat/createchatuseinput/{creatorId}", chatToBeCreated).Result.Content.ReadAsStringAsync();
                Console.WriteLine("ChatService row:56, result: " + result + ", type: "+result.GetType());

                return result;
            }
            catch (Exception e){
                Console.WriteLine("'Error!!! ChatService.cs: row:45. Error message: ");
                Console.WriteLine(e.Message + " ");
                Console.WriteLine("Stacktrace: "+ e.StackTrace);
                return null;
            }
        }
        
        
        
        
        
    }

    
    
    
    
    
    
    
    
    public interface IChatService{
        Task<List<ChatModel>> GetChats();
        Task<ChatModel> GetChatById(int id);
        Task<string> UpdateChat(ChatModel chat, int id);
        Task<string> CreateChat(ChatModel chat);

        Task<string> CreateChatUseInput(string creatorId, string chatName, Boolean encrypted,
            List<UserModel> joiners);
    }
}