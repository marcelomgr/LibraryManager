using LibraryManager.Infrastructure.Integrations.ApiCep.Models;

namespace LibraryManager.Infrastructure.Integrations.ApiCep.Interfaces
{
    public interface IApiCepService
    {
        Task<CepViewModel> GetByCep(string Cep);
    }
}
