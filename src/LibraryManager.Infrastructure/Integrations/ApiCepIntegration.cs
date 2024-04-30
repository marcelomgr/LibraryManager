using Newtonsoft.Json;
using LibraryManager.Core.Integrations.ApiCepIntegration;
using LibraryManager.Core.Integrations.ApiCepIntegration.Models;

namespace LibraryManager.Infrastructure.Integrations
{
    public class ApiCepIntegration : IApiCepService
    {
        private readonly HttpClient _httpClient;
        public ApiCepIntegration()
        {
            _httpClient = new HttpClient();
        }

        private string GetBaseUrl()
        {
            return "https://viacep.com.br/ws";
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
