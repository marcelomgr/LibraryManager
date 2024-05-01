using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetBooksAll
{
    public class GetBooksAllQueryHandler : IRequestHandler<GetBooksAllQuery, BaseResult<IEnumerable<Book>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksAllQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BaseResult<IEnumerable<Book>>> Handle(GetBooksAllQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();
            return new BaseResult<IEnumerable<Book>>(books);
        }
    }
}
