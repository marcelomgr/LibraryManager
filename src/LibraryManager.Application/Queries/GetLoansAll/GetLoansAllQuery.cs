using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;
using LibraryManager.Core.Dtos;

namespace LibraryManager.Application.Queries.GetLoansAll
{
    public class GetLoansAllQuery : IRequest<BaseResult<IEnumerable<LoanDTO>>>
    {
        public GetLoansAllQuery() { }
    }
}
