using MediatR;
using FluentValidation;
using LibraryManager.Core.Repositories;
using LibraryManager.Core.ValueObjects;
using LibraryManager.Application.Models;
using LibraryManager.Core.Integrations.ApiCepIntegration;
using LibraryManager.Core.Enums;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResult>
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<UpdateUserCommand> _validator;
        private readonly IApiCepService _apiCepService;

        public UpdateUserCommandHandler(IUserRepository repository, IValidator<UpdateUserCommand> validator, IApiCepService apiCepService)
        {
            _repository = repository;
            _validator = validator;
            _apiCepService = apiCepService;
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

            if (user.Location?.Cep != request.CEP)
            {
                var resultCep = await _apiCepService.GetByCep(request.CEP);

                if (resultCep == null)
                    return new BaseResult<Guid>(Guid.Empty, false, "CEP não encontrado.");

                var location = new LocationInfo(resultCep.Cep, resultCep.Logradouro, resultCep.Bairro, resultCep.Localidade, resultCep.UF);
                user.SetLocation(location);
            }

            user.Update(
                request.Name,
                request.CPF,
                request.Password,
                request.Email,
                Enum.Parse<UserRole>(request.Role.ToString()),
                user.Location);

            await _repository.UpdateAsync(user);

            return new BaseResult();
        }
    }
}

