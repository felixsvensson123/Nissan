using N_Chat.Shared;
using System.Net.Http.Json;

namespace N_Chat.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService (HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> LoginUser(LoginModel loginModel)
        {
            var result = await httpClient.PostAsJsonAsync<LoginModel>("api/user/login/", loginModel);
            if(result.IsSuccessStatusCode)
            {

                return await result.Content.ReadAsStringAsync();
            }

            return null;
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

        public async Task<string> GetUser(string id)
        {
            var result = await httpClient.GetFromJsonAsync<string>($"api/user/getuser/{id}");
            return result;
        }
        
    }

    public interface IUserService 
    {
        Task<string> LoginUser(LoginModel loginModel); // Login User Method
        Task<string> SignUp(RegisterModel registerModel); // Signup User Method
        Task<string> GetUser(string id);  // Get user by id method
    }
}
 