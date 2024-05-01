using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoanById
{
    public class GetLoanByIdQuery : IRequest<BaseResult<Loan>>
    {
        public GetLoanByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
