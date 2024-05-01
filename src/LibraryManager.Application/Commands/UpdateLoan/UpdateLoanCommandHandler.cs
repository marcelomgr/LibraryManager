using MediatR;
using LibraryManager.Core.Enums;
using Microsoft.EntityFrameworkCore;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Persistence;

namespace LibraryManager.Application.Commands.UpdateLoan
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, BaseResult>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly LibraryDbContext _context;

        public UpdateLoanCommandHandler(ILoanRepository loanRepository, IBookRepository bookRepository, LibraryDbContext context)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _context = context;
        }

        public async Task<BaseResult> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var loan = await _loanRepository.GetByIdAsync(request.Id);

                if (loan is null)
                {
                    await transaction.RollbackAsync();
                    return new BaseResult(false, "Empréstimo não encontrado.");
                }

                loan.UpdateReturnDate();

                await _loanRepository.UpdateAsync(loan);

                var book = await _bookRepository.GetByIdAsync(loan.BookId);

                if (book is not null)
                {
                    book.Devolution();

                    await _bookRepository.UpdateAsync(book);
                }
                else
                {
                    await transaction.RollbackAsync();
                    return new BaseResult(false, "Livro não encontrado.");
                }

                await transaction.CommitAsync();

                return new BaseResult();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResult(false, $"Ocorreu um erro ao atualizar o empréstimo: {ex.Message}");
            }
        }
    }
}
