using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.UpdateLoan
{
    public class UpdateLoanCommand : IRequest<BaseResult>
    {
        public Guid Id { get; set; }
    }
}
