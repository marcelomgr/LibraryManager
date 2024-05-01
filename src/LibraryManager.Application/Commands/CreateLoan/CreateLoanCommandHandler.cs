using MediatR;
using FluentValidation;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;
using LibraryManager.Infrastructure.Persistence.Repositories;
using LibraryManager.Infrastructure.Persistence;

namespace LibraryManager.Application.Commands.CreateLoan
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, BaseResult<Guid>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly LibraryDbContext _context;
        private readonly IValidator<CreateLoanCommand> _validator;

        public CreateLoanCommandHandler(ILoanRepository loanRepository, IValidator<CreateLoanCommand> validator, LibraryDbContext context, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _validator = validator;
            _context = context;
            _bookRepository = bookRepository;
        }

        public async Task<BaseResult<Guid>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errorMessages = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
                    await transaction.RollbackAsync();
                    return new BaseResult<Guid>(Guid.Empty, false, errorMessages);
                }

                var loan = new Loan(request.UserId, request.BookId);
                await _loanRepository.AddAsync(loan);

                var book = await _bookRepository.GetByIdAsync(loan.BookId);

                if (book is not null)
                {
                    if (book.Status == Core.Enums.BookStatus.Lent)
                    {
                        await transaction.RollbackAsync();
                        return new BaseResult<Guid>(Guid.Empty, false, "O livro está emprestado.");
                    }

                    book.Lent();

                    await _bookRepository.UpdateAsync(book);
                }
                else
                {
                    await transaction.RollbackAsync();
                    return new BaseResult<Guid>(Guid.Empty, false, "Livro não encontrado.");
                }

                await transaction.CommitAsync();

                return new BaseResult<Guid>(loan.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new BaseResult<Guid>(Guid.Empty, false, $"Ocorreu um erro ao atualizar o empréstimo: {ex.Message}");
            }
        }
    }
}
