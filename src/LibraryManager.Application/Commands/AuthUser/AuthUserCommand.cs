using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.AuthUser
{
    public class AuthUserCommand : IRequest<BaseResult<AuthViewModel>>
    {
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}
