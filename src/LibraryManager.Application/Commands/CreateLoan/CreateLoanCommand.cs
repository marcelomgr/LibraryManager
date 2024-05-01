using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<BaseResult<Guid>>
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
