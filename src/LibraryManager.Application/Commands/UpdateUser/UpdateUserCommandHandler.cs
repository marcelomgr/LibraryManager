using MediatR;
using FluentValidation;
using LibraryManager.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using LibraryManager.Core.Repositories;
using LibraryManager.Core.ValueObjects;
using LibraryManager.Application.Models;
using LibraryManager.Core.Services.AuthService;
using LibraryManager.Core.Integrations.ApiCepIntegration;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResult>
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<UpdateUserCommand> _validator;
        private readonly IApiCepService _apiCepService;
        private readonly IAuthService _authService;

        public UpdateUserCommandHandler(IUserRepository repository, IValidator<UpdateUserCommand> validator, IApiCepService apiCepService, IAuthService authService)
        {
            _repository = repository;
            _validator = validator;
            _apiCepService = apiCepService;
            _authService = authService;
        }

        public async Task<BaseResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

            var user = await _repository.GetByIdAsync(request.Id);

            request.Password = _authService.HashPassword(request.Password);

            if (user.CPF != request.CPF)
            {
                var existingUser = await _repository.GetByCpfAsync(request.CPF);

                if (existingUser != null)
                    return new BaseResult<Guid>(Guid.Empty, false, "CPF já cadastrado.");
            }

            if (!request.Password.IsNullOrEmpty())
                user.Password = _authService.HashPassword(request.Password);

            if (user.Location?.Cep != request.CEP)
            {
                var resultCep = await _apiCepService.GetByCep(request.CEP);

                if (resultCep is null)
                    return new BaseResult<Guid>(Guid.Empty, false, "CEP não encontrado.");

                var location = new LocationInfo(resultCep.Cep, resultCep.Logradouro, resultCep.Bairro, resultCep.Localidade, resultCep.UF);
                user.SetLocation(location);
            }

            user.Update(
                request.Name,
                request.CPF,
                user.Password,
                request.Email,
                Enum.Parse<UserRole>(request.Role.ToString()),
                user.Location);

            await _repository.UpdateAsync(user);

            return new BaseResult();
        }
    }
}
