﻿using MediatR;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using LibraryManager.Core.ValueObjects;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;
using LibraryManager.Core.Services.AuthService;
using LibraryManager.Core.Integrations.ApiCepIntegration;

namespace LibraryManager.Application.Commands.SignUpUser
{
    public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, BaseResult<Guid>>
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<SignUpUserCommand> _validator;
        private readonly IApiCepService _apiCepService;
        private readonly IAuthService _authService;

        public SignUpUserCommandHandler(IUserRepository repository, IValidator<SignUpUserCommand> validator, IApiCepService apiCepService, IAuthService authService)
        {
            _repository = repository;
            _validator = validator;
            _apiCepService = apiCepService;
            _authService = authService;
        }

        public async Task<BaseResult<Guid>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

            var existingUser = await _repository.GetByCpfAsync(request.CPF.Trim().Replace(".", "").Replace("-", ""));

            if (existingUser is not null)
                return new BaseResult<Guid>(Guid.Empty, false, "CPF já cadastrado.");

            request.Password = _authService.HashPassword(request.Password);

            var user = request.ToEntity();

            if (request.CEP is not null && !request.CEP.IsNullOrEmpty())
            {
                var resultCep = await _apiCepService.GetByCep(request.CEP);

                if (resultCep is null)
                    return new BaseResult<Guid>(Guid.Empty, false, "CEP não encontrado.");

                var location = new LocationInfo(resultCep.Cep, resultCep.Logradouro, resultCep.Bairro, resultCep.Localidade, resultCep.UF);
                user.SetLocation(location);
            }

            await _repository.AddAsync(user);

            return new BaseResult<Guid>(user.Id);
        }
    }
}
