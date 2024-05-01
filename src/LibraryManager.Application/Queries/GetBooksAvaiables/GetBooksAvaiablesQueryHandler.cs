using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetBooksAvaiables
{
    public class GetBooksAvailableQueryHandler : IRequestHandler<GetBooksAvailableQuery, BaseResult<IEnumerable<Book>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBooksAvailableQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BaseResult<IEnumerable<Book>>> Handle(GetBooksAvailableQuery request, CancellationToken cancellationToken)
        {
            var availableBooks = await _bookRepository.GetAvailableBooksAsync();
            return new BaseResult<IEnumerable<Book>>(availableBooks);
        }
    }
}
