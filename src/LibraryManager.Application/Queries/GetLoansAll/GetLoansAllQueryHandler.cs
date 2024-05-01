using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansAll
{
    public class GetLoansAllQueryHandler : IRequestHandler<GetLoansAllQuery, BaseResult<IEnumerable<Loan>>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoansAllQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<BaseResult<IEnumerable<Loan>>> Handle(GetLoansAllQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync();
            return new BaseResult<IEnumerable<Loan>>(loans);
        }
    }
}
