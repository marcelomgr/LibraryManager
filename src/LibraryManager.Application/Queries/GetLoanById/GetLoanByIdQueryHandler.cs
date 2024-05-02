using MediatR;
using AutoMapper;
using LibraryManager.Core.Dtos;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoanById
{
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, BaseResult<LoanDTO>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoanByIdQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<BaseResult<LoanDTO>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(request.Id, l => l.User, l => l.Book);
            var loanDTO = _mapper.Map<LoanDTO>(loan);

            if (loan is null)
                return new BaseResult<LoanDTO>(null, false, "Empréstimo não encontrado.");

            return new BaseResult<LoanDTO>(loanDTO);
        }
    }
}
