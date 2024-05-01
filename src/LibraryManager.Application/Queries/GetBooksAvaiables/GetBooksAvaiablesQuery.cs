using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetBooksAvaiables
{
    public class GetBooksAvailableQuery : IRequest<BaseResult<IEnumerable<Book>>>
    {
    }
}
