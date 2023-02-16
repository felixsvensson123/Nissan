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

        // This function is responsible for login and it receives a LoginModel object. 
        public async Task<string> LoginUser(LoginModel loginModel)
        {
            var result = await httpClient.PostAsJsonAsync("api/user/login/", loginModel);
            
            if(result.IsSuccessStatusCode)
            {
                return "Success";
            }
            
            //response says  PasswordTooShort
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "BadRequest, could be PasswordTooShort. Is password minimum 6 characters?";
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
        public async Task<UserModel> GetUser(string userName)
        {
            var result = await httpClient.GetFromJsonAsync<UserModel>($"api/user/{userName}");
            return result;
        }

        public async Task<UserModel> GetUserByName(string name)
        {
            var result = await httpClient.GetFromJsonAsync<UserModel>($"api/user/getbyname/{name}");
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

        public async Task<ICollection<UserModel>> GetAllUsers()
        {
            return await httpClient.GetFromJsonAsync<ICollection<UserModel>>($"api/user/chats/");
        }
        
        public async Task<string> AddUserToChat(UserModel user, int chatId)
        {
            var result = await httpClient.PostAsJsonAsync($"api/user/chatrequest/{chatId}", user);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }
            return null;
        }
        
        public async Task<Task<string>> UpdateUser(UserModel user, string userName)
        {
            var result = await httpClient.PutAsJsonAsync<UserModel>("api/user/softdeleteuser/{userName}", user);
            return result.Content.ReadAsStringAsync();
        }
    }

    public interface IUserService 
    {
        Task<string> LoginUser(LoginModel loginModel); // Login User Method
        Task<string> SignUp(RegisterModel registerModel); // Signup User Method
        Task<UserModel> GetUserByName(string name);// Get user by username method
        Task<string> Signout(); // Signout user
        Task<(string Message, UserModel? user)> GetUserClaim(); //Gets user via claims
        Task<UserModel> GetUser(string userName); // gets specific user
        Task<ICollection<UserModel>> GetAllUsers(); //Gets All Users 
        Task<string> AddUserToChat(UserModel user, int chatId);
        Task<Task<string>> UpdateUser(UserModel user, string userName);
    }
}
 