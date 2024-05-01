using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansByBookId
{
    public class GetLoansByBookIdQueryHandler : IRequestHandler<GetLoansByBookIdQuery, BaseResult<IEnumerable<Loan>>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoansByBookIdQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<BaseResult<IEnumerable<Loan>>> Handle(GetLoansByBookIdQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetLoansByBookIdAsync(request.BookId);

            return new BaseResult<IEnumerable<Loan>>(loans);
        }
    }
}
