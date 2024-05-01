using MediatR;
using FluentValidation;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BaseResult<Guid>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<CreateBookCommand> _validator;

        public CreateBookCommandHandler(IBookRepository bookRepository, IValidator<CreateBookCommand> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
        }

        public async Task<BaseResult<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

            var book = new Book(request.Title, request.Author, request.ISBN, request.PublicationYear);

            await _bookRepository.AddAsync(book);

            return new BaseResult<Guid>(book.Id);
        }
    }
}
