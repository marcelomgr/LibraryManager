using MediatR;
using FluentValidation;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BaseResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateBookCommand> _validator;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateBookCommand> validator)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult<Guid>(book.Id);
        }
    }
}
