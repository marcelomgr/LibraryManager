using System;
using LibraryManager.Application.Models;
using LibraryManager.Core.Entities;
using MediatR;

namespace LibraryManager.Application.Commands.SignUpUser
{
	public class SignUpUserCommand : IRequest<BaseResult<Guid>>
	{
		public string FirstName { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string CPF { get; set; }

		public User ToEntity() => new User(FirstName, FullName, Email, CPF);
	}
}

