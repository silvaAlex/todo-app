using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ToDoApp.Web.Client
{
    public class ApiClient
    {
        private static readonly Lazy<HttpClient> _lazyClient = new Lazy<HttpClient>(() =>
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

            if (string.IsNullOrEmpty(baseUrl))
                throw new ConfigurationErrorsException("ApiBaseUrl não foi configurada no Web.Config");

            return new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        });

        public HttpClient Client = _lazyClient.Value;

        public void AddAuthorization()
        {
            var token = Helpers.AuthSession.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}