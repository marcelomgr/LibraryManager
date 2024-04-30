using MediatR;
using LibraryManager.Core.Entities;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Commands.SignUpUser
{
    public class SignUpUserCommand : IRequest<BaseResult<Guid>>
	{
        public string Name { get; set; }
		public string CPF { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
		public string CEP { get; set; }
		public string Role { get; set; }

		public User ToEntity() => new User(Name, CPF, Password, Email, Role); //Enum.Parse<UserRole>(Role.ToString())
    }
}

