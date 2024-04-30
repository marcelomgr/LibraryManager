using MediatR;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using LibraryManager.Core.ValueObjects;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Integrations.ApiCep.Interfaces;

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

            if (request.CEP != null && !request.CEP.IsNullOrEmpty())
            {
                var resultCep = await _apiCepService.GetByCep(request.CEP);

                if (resultCep == null)
                    return new BaseResult<Guid>(Guid.Empty, false, "CEP não encontrado.");

                var location = new LocationInfo(resultCep.Cep, resultCep.Logradouro, resultCep.Bairro, resultCep.Localidade, resultCep.UF);
                user.SetLocation(location);
            }

            await _repository.AddAsync(user);

            return new BaseResult<Guid>(user.Id);
        }
    }
}

