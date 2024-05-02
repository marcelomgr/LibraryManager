using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Core.Repositories;
using LibraryManager.Application.Models;
using LibraryManager.Core.Services.AuthService;

namespace LibraryManager.Application.Commands.AuthUser
{
    public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, BaseResult<AuthViewModel>>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _repository;

        public AuthUserCommandHandler(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<BaseResult<AuthViewModel>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            if (request.CPF.Length is 0 || request.Password.Length is 0)
                return new BaseResult<AuthViewModel>(null, false, "Informe usuário e senha.");

            var passwordHash = User.HashPassword(request.Password);
            var user = await _repository.ValidateUserCredentialsAsync(request.CPF, passwordHash);

            if (user is null)
                return new BaseResult<AuthViewModel>(null, false, "Usuário ou senha inválidos.");

            var jwtToken = _authService.GenerateJwtToken(user);
            var viewModel = new AuthViewModel(user.Name, user.CPF, jwtToken);

            return new BaseResult<AuthViewModel>(viewModel);
        }
    }
}
