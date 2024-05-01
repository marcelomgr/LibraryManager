using FluentValidation;
using LibraryManager.Utilities;
using LibraryManager.Core.Enums;
using LibraryManager.Application.Commands.SignUpUser;

namespace LibraryManager.Application.Validators
{
    public class SignUpUserValidator : AbstractValidator<CreateUserCommand>
    {
        public SignUpUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.CPF)
                .NotEmpty()
                .Custom((cpf, context) =>
                {
                    if (!Utils.IsCpfValid(cpf))
                    {
                        context.AddFailure("CPF", "CPF inválido");
                    }
                });

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => Enum.TryParse<UserRole>(role, out _))
                .WithMessage("Role inválida");
        }
    }
}
