using N_Chat.Shared;
using System.Net.Http.Json;

namespace N_Chat.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public UserService (HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClient;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> LoginUser(LoginModel loginModel)
        {
            var result = await httpClient.PostAsJsonAsync("api/user/login/", loginModel);
            if(result.IsSuccessStatusCode)
            {
                return "Success";
            }
            return null;
        }
        public async Task<string> Signout()
        {
            var reponse = await httpClient.PostAsync("api/user/logout", null);
            if (reponse.IsSuccessStatusCode)
            {
                return "Sign Out succeded!";
            }

            return "Sign Out Failed!";
        }
        public async Task<string> SignUp(RegisterModel registerModel)
        {
            var result = await httpClient.PostAsJsonAsync<RegisterModel>("api/user/signup", registerModel);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var result = await httpClient.GetFromJsonAsync<UserModel>($"api/user/get/{id}");
            return result;
        }

        public async Task<(string Message, UserModel? user)> GetUserClaim()
        {
            var response = await httpClient.GetAsync("api/user/getcurrent");
            if (response.IsSuccessStatusCode)
            {
                return ("Success", await response.Content.ReadFromJsonAsync<UserModel>());
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return ("Unauthorized", null);
                }
                else
                {
                    return ("Failed", null);
                }
            }
        }

        public async Task<List<ChatModel>> GetUserChats(string id)
        {
            return await httpClient.GetFromJsonAsync<List<ChatModel>>($"api/user/userchats/{id}");
        }

        public async Task<string> AddUserToChat(string userName, int chatId)
        {
            var result = await httpClient.PutAsJsonAsync($"api/user/chatrequest/{chatId}", userName);
            if (result.IsSuccessStatusCode)
            {
                return "Success!";
            }
            return "Failed";
        }
    }

    public interface IUserService 
    {
        Task<string> LoginUser(LoginModel loginModel); // Login User Method
        Task<string> SignUp(RegisterModel registerModel); // Signup User Method
        Task<UserModel> GetUserById(string id);  // Get user by id method
        Task<string> Signout(); // Signout user
        Task<(string Message, UserModel? user)> GetUserClaim(); //Gets user via claims
        Task<List<ChatModel>> GetUserChats(string id);
        Task<string> AddUserToChat(string userName, int chatId);
    }
}
 