using LibraryManager.Application.Models;
using LibraryManager.Core.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.GetUsersAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseResult<List<GetAllUsersViewModel>>>
    {
        private readonly IUserRepository _repository;
        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<BaseResult<List<GetAllUsersViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();

            if (users is null)
            {
                return new BaseResult<List<GetAllUsersViewModel>>(null, false, "Nenhum usuário encontrado");
            }

            var viewModels = users.Select(user => new GetAllUsersViewModel(user)).ToList();

            return new BaseResult<List<GetAllUsersViewModel>>(viewModels);
        }
    }
}
