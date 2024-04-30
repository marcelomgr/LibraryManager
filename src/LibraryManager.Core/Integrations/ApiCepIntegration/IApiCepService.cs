using LibraryManager.Core.Integrations.ApiCepIntegration.Models;

namespace LibraryManager.Core.Integrations.ApiCepIntegration
{
    public interface IApiCepService
    {
        Task<CepViewModel?> GetByCep(string Cep);
    }
}
