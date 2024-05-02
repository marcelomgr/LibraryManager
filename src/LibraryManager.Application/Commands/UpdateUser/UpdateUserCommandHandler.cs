using MediatR;
using FluentValidation;
using LibraryManager.Core.Enums;
using LibraryManager.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using LibraryManager.Core.Repositories;
using LibraryManager.Core.ValueObjects;
using LibraryManager.Application.Models;
using LibraryManager.Core.Integrations.ApiCepIntegration;

namespace LibraryManager.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateUserCommand> _validator;
        private readonly IApiCepService _apiCepService;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateUserCommand> validator, IApiCepService apiCepService)
        {
            _unitOfWork = unitOfWork;
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

            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);

            if (user is null)
                return new BaseResult<Guid>(Guid.Empty, false, "Usuário não encontrado.");

            request.CPF = User.NormalizeCPF(request.CPF);

            if (user.CPF != request.CPF)
            {
                var existingUser = await _unitOfWork.Users.GetByCpfAsync(request.CPF);

                if (existingUser is not null)
                    return new BaseResult<Guid>(Guid.Empty, false, "CPF já cadastrado.");
            }

            if (!request.Password.IsNullOrEmpty())
                user.Password = User.HashPassword(request.Password);

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

            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult();
        }
    }
}
