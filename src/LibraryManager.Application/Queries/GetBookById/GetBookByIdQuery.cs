using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetBookById
{
    public class GetBookByIdQuery : IRequest<BaseResult<Book>>
    {
        public GetBookByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
