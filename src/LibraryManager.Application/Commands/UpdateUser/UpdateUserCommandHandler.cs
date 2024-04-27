using MediatR;
using LibraryManager.Application.Models;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResult>
	{
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
		{
            _repository = repository;
        }

        public async Task<BaseResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            user.Update(
                request.FirstName,
                request.FullName,
                request.Email,
                request.CPF);

            await _repository.UpdateAsync(user);

            return new BaseResult();
        }
    }
}

