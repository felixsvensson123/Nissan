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

        public async Task<string> LoginUser(LoginModel user)
        {
            var result = await httpClient.PostAsJsonAsync<LoginModel>("api/user/login/", user);
            if(result.IsSuccessStatusCode)
            {

                return await result.Content.ReadAsStringAsync();
            }
            return result.StatusCode.ToString();
        }

        public async Task<string> SignUp(RegisterModel user)
        {
            var result = await httpClient.PostAsJsonAsync<RegisterModel>("api/user/signup", user);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();
            }

            return result.StatusCode.ToString();
        }
    }

    public interface IUserService
    {
        Task<string> LoginUser(LoginModel user);
    }
}
