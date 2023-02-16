using N_Chat.Shared;
using System.Net.Http.Json;

namespace N_Chat.Client.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ICollection<ChatModel>> GetChats()
        {
            var results = await _httpClient.GetFromJsonAsync<ICollection<ChatModel>>("api/chat/getall");
            return results;
        }

        public async Task<ChatModel> GetChatById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ChatModel>($"api/chat/getbyid/{id}");
            return result;
        }

        public async Task<string> UpdateChat(ChatModel chat, int id)
        {
            var result = await _httpClient.PutAsJsonAsync<ChatModel>("api/chat/updatechat/{id}", chat);
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateChat(ChatModel chat)
        {
            var result = await _httpClient.PostAsJsonAsync("api/chat/createchat", chat);
            
            if (result.IsSuccessStatusCode)
                return await result.Content?.ReadAsStringAsync();

            return "Failure";
        }
    }

    public interface IChatService
    {
        Task<ICollection<ChatModel>> GetChats();
        Task<ChatModel> GetChatById(int id);
        Task<string> UpdateChat(ChatModel chat, int id);
        Task<string> CreateChat(ChatModel chat);
    }
}
