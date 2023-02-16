using System.Collections;
using System.Net.Http.Json;





namespace N_Chat.Client.Services
{
    public class MessageService:IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MessageModel>> GetAllMessages()
        {
            var respons = await _httpClient.GetFromJsonAsync<MessageModel>($"api/message/getallmessages");
            if(respons != null)
            {
                return (IEnumerable<MessageModel>)respons;    
            }
            return null;
        }

        public async Task<IEnumerable<MessageModel>> GetAllUserMessages(string id)
        {
            var respons = await _httpClient.GetFromJsonAsync<MessageModel>($"api/message/usermessages/{id}");
            if(respons != null)
            {
                return (IEnumerable<MessageModel>)respons;
            }
            return null;
        }

        public async Task<MessageModel>GetById(int id)
        {
            var respons = await _httpClient.GetFromJsonAsync<MessageModel>($"api/message/{id}");
            if( respons != null)
            {
                return respons;
            }
            return null;
        }

        public async Task<string> PostMessage(MessageModel messageModel)
        {
            var respons = await _httpClient.PostAsJsonAsync<MessageModel>($"api/message/postmessage",messageModel);
            if(respons != null)
            {
                return await respons.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string> PutMessage(MessageModel messageModel, int id)
        {
            var respons = await _httpClient.PutAsJsonAsync<MessageModel>($"api/message/updatemessage/{id}",messageModel);
            if (respons != null)
            {
                return await respons.Content.ReadAsStringAsync(); 
            }
            return null;
        }

        public async Task<ICollection<MessageModel>> GetChatMessages(int chatId)
        {
            return await _httpClient.GetFromJsonAsync<ICollection<MessageModel>>($"api/message/GetChatMessages/{chatId}");
        }


    }

    public interface IMessageService
    {
        Task<IEnumerable<MessageModel>> GetAllUserMessages(string id);
        Task<MessageModel> GetById(int id);       
        Task<string> PutMessage(MessageModel messageModel, int id);
        Task<IEnumerable<MessageModel>> GetAllMessages();
        Task<ICollection<MessageModel>> GetChatMessages(int chatId);
        Task<string> PostMessage(MessageModel messageModel);
    }
}
