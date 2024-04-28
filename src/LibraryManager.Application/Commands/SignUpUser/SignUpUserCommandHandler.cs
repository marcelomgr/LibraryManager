using MediatR;
using FluentValidation;
using LibraryManager.Application.Models;
using LibraryManager.Core.Repositories;

namespace LibraryManager.Application.Commands.SignUpUser
{
    public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, BaseResult<Guid>>
	{
        private readonly IUserRepository _repository;
        private readonly IValidator<SignUpUserCommand> _validator;

        public SignUpUserCommandHandler(IUserRepository repository, IValidator<SignUpUserCommand> validator)
		{
            _repository = repository;
            _validator = validator;
        }

        public async Task<BaseResult<Guid>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

            var user = request.ToEntity();

            await _repository.AddAsync(user);

            return new BaseResult<Guid>(user.Id);
        }
    }
}

