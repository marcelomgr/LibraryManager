using Newtonsoft.Json;
using LibraryManager.Infrastructure.Integrations.ApiCep.Models;
using LibraryManager.Infrastructure.Integrations.ApiCep.Interfaces;

namespace LibraryManager.Infrastructure.Integrations.ApiCep.Services
{
    public class ApiCepService : IApiCepService
    {
        private readonly HttpClient _httpClient;
        public ApiCepService()
        {
            _httpClient = new HttpClient();
        }

        private string GetBaseUrl()
        {
            return "https://viacep.com.br/ws";
            //return "https://viacep.com.br/ws/06462260/json";
        }

        public async Task<CepViewModel?> GetByCep(string Cep)
        {
            string authServiceUrl = $"{GetBaseUrl()}/{Cep}/json";

            HttpResponseMessage response = await _httpClient.GetAsync(authServiceUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                CepViewModel cepResponse = JsonConvert.DeserializeObject<CepViewModel>(content);
                return cepResponse;
            }

            return null;
        }
    }
}
