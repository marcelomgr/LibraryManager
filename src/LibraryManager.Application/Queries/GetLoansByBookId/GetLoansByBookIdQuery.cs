using MediatR;
using LibraryManager.Core.Dtos;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansByBookId
{
    public class GetLoansByBookIdQuery : IRequest<BaseResult<IEnumerable<LoanDTO>>>
    {
        public Guid BookId { get; }

        public GetLoansByBookIdQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
