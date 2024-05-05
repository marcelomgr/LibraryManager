using MediatR;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateLoan
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLoanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(request.Id);

            if (loan is null)
            {
                return new BaseResult(false, "Empréstimo não encontrado.");
            }

            loan.UpdateReturnDate();

            await _unitOfWork.Loans.UpdateAsync(loan);

            var book = await _unitOfWork.Books.GetByIdAsync(loan.BookId);

            if (book is null)
            {
                return new BaseResult(false, "Livro não encontrado.");
            }

            book.Devolution();

            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult();
        }
    }
}
