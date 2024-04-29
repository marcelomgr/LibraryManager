using System;
using LibraryManager.Application.Models;
using MediatR;

namespace LibraryManager.Application.Commands.UpdateUser
{
	public class UpdateUserCommand : IRequest<BaseResult>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string CPF { get; set; }
        public LocationInfoModel Location { get; set; }
    }
}

