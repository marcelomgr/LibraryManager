using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<BaseResult>
    {
        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
