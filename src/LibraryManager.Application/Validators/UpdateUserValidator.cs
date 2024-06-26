﻿using FluentValidation;
using LibraryManager.Utilities;
using LibraryManager.Core.Enums;
using LibraryManager.Application.Commands.UpdateUser;

namespace LibraryManager.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.CPF)
                .NotEmpty()
                .Custom((cpf, context) =>
                {
                    if (!Utils.IsCpfValid(cpf))
                    {
                        context.AddFailure("CPF", "CPF inválido");
                    }
                });

            RuleFor(x => x.CEP)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => Enum.TryParse<UserRole>(role, out _))
                .WithMessage("Role inválida");
        }
    }
}
