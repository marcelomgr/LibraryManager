using MediatR;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateLoan
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, BaseResult>
    {
        private readonly ILoanRepository _loanRepository;

        public UpdateLoanCommandHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<BaseResult> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id);

            if (loan is null)
                return new BaseResult(false, "Empréstimo não encontrado.");

            loan.UpdateReturnDate(DateTime.UtcNow);

            await _loanRepository.UpdateAsync(loan);
            
            return new BaseResult();
        }
    }

}
