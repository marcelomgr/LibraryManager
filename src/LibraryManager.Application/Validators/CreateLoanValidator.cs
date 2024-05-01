using FluentValidation;
using LibraryManager.Application.Commands.CreateLoan;

namespace LibraryManager.Application.Validators
{
    public class CreateLoanValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.BookId)
                .NotEmpty();
        }
    }
}
