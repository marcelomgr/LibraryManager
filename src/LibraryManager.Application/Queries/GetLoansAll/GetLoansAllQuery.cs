using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansAll
{
    public class GetLoansAllQuery : IRequest<BaseResult<IEnumerable<Loan>>>
    {
        public GetLoansAllQuery() { }
    }
}
