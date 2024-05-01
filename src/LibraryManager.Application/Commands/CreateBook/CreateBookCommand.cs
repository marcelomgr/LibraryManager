using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<BaseResult<Guid>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
    }
}
