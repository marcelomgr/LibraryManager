using FluentValidation;
using LibraryManager.Utilities;
using LibraryManager.Application.Commands.UpdateUser;

namespace LibraryManager.Application.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.FullName)
                .NotEmpty();

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
        }
    }
}
