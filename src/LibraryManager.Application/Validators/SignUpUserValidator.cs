using FluentValidation;
using LibraryManager.Utilities;
using LibraryManager.Application.Commands.SignUpUser;

namespace LibraryManager.Application.Validators
{
    public class SignUpUserValidator : AbstractValidator<SignUpUserCommand>
    {
        public SignUpUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.FullName)
                .NotEmpty();

            RuleFor(x => x.CPF)
                .NotEmpty()
                .Custom((cpf, context) =>
                {
                    if (!Utils.IsCpfValid(cpf))
                    {
                        context.AddFailure("CPF", "CPF inválido");
                    }
                });
        }
    }
}
