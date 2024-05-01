using MediatR;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetUserByCpf
{
    public class GetUserByCpfQueryHandler : IRequestHandler<GetUserByCpfQuery, BaseResult<GetUserByCpfViewModel>>
    {
        private readonly IUserRepository _repository;
        public GetUserByCpfQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResult<GetUserByCpfViewModel>> Handle(GetUserByCpfQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByCpfAsync(request.Cpf);

            if (user is null)
            {
                return new BaseResult<GetUserByCpfViewModel>(null, false, string.Empty);
            }

            var viewModel = new GetUserByCpfViewModel(user);

            return new BaseResult<GetUserByCpfViewModel>(viewModel);
        }
    }
}
