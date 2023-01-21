using N_Chat.Shared;
using System.Net.Http.Json;

namespace N_Chat.Client.Services
{
    public class TestService : ITestService
    {
        private readonly HttpClient _httpClient;

        public TestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TestModel>> GetTest()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TestModel>>("api/test/");
            return result;
        }
    }


    public interface ITestService
    {
        Task<List<TestModel>> GetTest();
    }
}
