using MediatR;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;
using LibraryManager.Application.Commands.DeleteBook;

namespace LibraryManager.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteBookCommand, BaseResult>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                return new BaseResult(false, "Usuario não encontrado.");

            await _userRepository.DeleteAsync(user.Id);

            return new BaseResult();
        }
    }
}
