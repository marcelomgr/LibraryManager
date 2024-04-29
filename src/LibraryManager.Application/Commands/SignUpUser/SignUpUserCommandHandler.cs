using LibraryManager.Application.Models;
using LibraryManager.Core.Repositories;
using LibraryManager.Infrastructure.Integrations.ApiCep.Interfaces;
using MediatR;
using LibraryManager.Core.ValueObjects;
using FluentValidation;

namespace LibraryManager.Application.Commands.SignUpUser
{
    public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, BaseResult<Guid>>
	{
        private readonly IUserRepository _repository;
        private readonly IValidator<SignUpUserCommand> _validator;
        private readonly IApiCepService _apiCepService;

        public SignUpUserCommandHandler(IUserRepository repository, IValidator<SignUpUserCommand> validator, IApiCepService apiCepService)
		{
            _repository = repository;
            _validator = validator;
            _apiCepService = apiCepService;
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

            var resultCep = await _apiCepService.GetByCep(request.CEP);

            if (resultCep != null)
            {
                var location = new LocationInfo(resultCep.Cep, resultCep.Logradouro, resultCep.Bairro, resultCep.Localidade, resultCep.UF);
                user.SetLocation(location);
            }

            await _repository.AddAsync(user);

            return new BaseResult<Guid>(user.Id);
        }
    }
}

