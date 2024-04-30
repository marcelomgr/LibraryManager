using MediatR;
using LibraryManager.Application.Models;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResult<GetUserByIdViewModel>>
    {
        private readonly IUserRepository _repository;
        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<BaseResult<GetUserByIdViewModel?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user is null) {
                return new BaseResult<GetUserByIdViewModel?>(null, false, string.Empty);
            }

            var viewModel = new GetUserByIdViewModel(user);

            return new BaseResult<GetUserByIdViewModel?>(viewModel);
        }
    }
}

