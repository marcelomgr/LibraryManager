using MediatR;
using FluentValidation;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.CreateLoan
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, BaseResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateLoanCommand> _validator;

        public CreateLoanCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateLoanCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<BaseResult<Guid>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
            }

            var loan = new Loan(request.UserId, request.BookId);
            await _unitOfWork.Loans.AddAsync(loan);

            var book = await _unitOfWork.Books.GetByIdAsync(loan.BookId);

            if (book is not null)
            {
                if (book.Status == Core.Enums.BookStatus.Lent)
                {
                    return new BaseResult<Guid>(Guid.Empty, false, "O livro está emprestado.");
                }

                book.Lent();

                await _unitOfWork.Books.UpdateAsync(book);
            }
            else
            {
                return new BaseResult<Guid>(Guid.Empty, false, "Livro não encontrado.");
            }

            await _unitOfWork.SaveChangesAsync();

            return new BaseResult<Guid>(loan.Id);
        }
    }
}
