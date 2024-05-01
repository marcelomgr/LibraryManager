using MediatR;
using FluentValidation;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BaseResult>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<UpdateBookCommand> _validator;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IValidator<UpdateBookCommand> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
        }

        public async Task<BaseResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book is null)
                return new BaseResult(false, "Livro não encontrado.");

            book.Update(request.Title, request.Author, request.ISBN, request.PublicationYear);

            await _bookRepository.UpdateAsync(book);
            
            return new BaseResult();
        }
    }

}
