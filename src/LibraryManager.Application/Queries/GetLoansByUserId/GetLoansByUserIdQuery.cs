using MediatR;
using LibraryManager.Core.Dtos;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansByUserId
{
    public class GetLoansByUserIdQuery : IRequest<BaseResult<IEnumerable<LoanDTO>>>
    {
        public Guid UserId { get; }

        public GetLoansByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
