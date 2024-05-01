﻿using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansByUserId
{
    public class GetLoansByUserIdQueryHandler : IRequestHandler<GetLoansByUserIdQuery, BaseResult<IEnumerable<Loan>>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetLoansByUserIdQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<BaseResult<IEnumerable<Loan>>> Handle(GetLoansByUserIdQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetLoansByUserIdAsync(request.UserId);
            return new BaseResult<IEnumerable<Loan>>(loans);
        }
    }
}