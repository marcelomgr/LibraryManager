using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResult>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
