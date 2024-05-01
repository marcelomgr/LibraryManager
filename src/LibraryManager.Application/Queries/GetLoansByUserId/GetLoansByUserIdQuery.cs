using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansByUserId
{
    public class GetLoansByUserIdQuery : IRequest<BaseResult<IEnumerable<Loan>>>
    {
        public Guid UserId { get; }

        public GetLoansByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
