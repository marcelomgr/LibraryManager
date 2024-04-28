using MediatR;
using FluentValidation;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResult>
	{
        private readonly IUserRepository _repository;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserCommandHandler(IUserRepository repository, IValidator<UpdateUserCommand> validator)
		{
            _repository = repository;
            _validator = validator;
        }

        public async Task<BaseResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

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

