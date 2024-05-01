using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoansByBookId
{
    public class GetLoansByBookIdQuery : IRequest<BaseResult<IEnumerable<Loan>>>
    {
        public Guid BookId { get; }

        public GetLoansByBookIdQuery(Guid bookId)
        {
            BookId = bookId;
        }
    }
}
