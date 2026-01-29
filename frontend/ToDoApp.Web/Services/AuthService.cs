using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Web.Client;
using ToDoApp.Web.Models.Auth;
using ToDoApp.Web.Models.Register;

namespace ToDoApp.Web.Services
{
    public class AuthService
    {
        public async Task<string> LoginAsync(LoginRequest request)
        {
            var api = new ApiClient();

            var jsonContent = JsonConvert.SerializeObject(request);

            var content = new StringContent(
                jsonContent,
                Encoding.UTF8,
                "application/json"
            );

            var response = await api.Client.PostAsync("api/auth/login", content).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var erroContent = await response.Content.ReadAsStringAsync();
                throw new System.Exception($"Status: {response.StatusCode}, Erro: {erroContent}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponse>(json);

            return result.Token;
        }

        public async Task<bool> CreateAsync(RegisterRequest request)
        {
            var api = new ApiClient();

            var json = JsonConvert.SerializeObject(request);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await api.Client.PostAsync("api/auth/register", content);

            return response.IsSuccessStatusCode;
        }
    }
}