using LibraryManager.Application.Models;
using MediatR;

namespace LibraryManager.Application.Queries.GetUserByCpf
{
    public class GetUserByCpfQuery : IRequest<BaseResult<GetUserByCpfViewModel>>
    {
        public GetUserByCpfQuery(string cpf)
        {
            Cpf = cpf;
        }

        public string Cpf { get; private set; }
    }
}
