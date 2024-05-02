using MediatR;
using LibraryManager.Core.Dtos;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetLoanById
{
    public class GetLoanByIdQuery : IRequest<BaseResult<LoanDTO>>
    {
        public GetLoanByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
