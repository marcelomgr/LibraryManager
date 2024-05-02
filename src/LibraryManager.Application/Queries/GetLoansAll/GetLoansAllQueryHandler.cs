using MediatR;
using AutoMapper;
using LibraryManager.Core.Dtos;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansAll
{
    public class GetLoansAllQueryHandler : IRequestHandler<GetLoansAllQuery, BaseResult<IEnumerable<LoanDTO>>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoansAllQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<BaseResult<IEnumerable<LoanDTO>>> Handle(GetLoansAllQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync(l => l.User, l => l.Book);
            var loanDTOs = _mapper.Map<IEnumerable<LoanDTO>>(loans);

            return new BaseResult<IEnumerable<LoanDTO>>(loanDTOs);
        }
    }
}
