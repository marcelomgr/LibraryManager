using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetBooksAll
{
    public class GetBooksAllQuery : IRequest<BaseResult<IEnumerable<Book>>>
    {
    }
}
