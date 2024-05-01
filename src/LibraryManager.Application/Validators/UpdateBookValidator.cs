using FluentValidation;
using LibraryManager.Application.Commands.UpdateBook;

namespace LibraryManager.Application.Validators
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.Author)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .MinimumLength(13);

            RuleFor(x => x.PublicationYear)
                .NotEmpty()
                .GreaterThan(0)
                .Must(year => year >= 1000 && year <= 3000)
                .WithMessage("Ano de publicação inválido.");
        }
    }
}
