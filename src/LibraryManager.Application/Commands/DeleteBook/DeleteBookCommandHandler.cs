using MediatR;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);

            if (book is null)
                return new BaseResult(false, "Livro não encontrado.");

            await _unitOfWork.Books.DeleteAsync(book.Id);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResult();
        }
    }
}
