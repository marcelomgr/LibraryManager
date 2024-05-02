using MediatR;
using FluentValidation;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateBookCommand> _validator;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateBookCommand> validator)
        {
            _unitOfWork = unitOfWork;
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

            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);

            if (book is null)
                return new BaseResult(false, "Livro não encontrado.");

            book.Update(request.Title, request.Author, request.ISBN, request.PublicationYear);

            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult();
        }
    }
}
