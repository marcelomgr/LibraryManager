using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<BaseResult<GetUserByIdViewModel>>
	{
		public GetUserByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; private set; }
	}
}

