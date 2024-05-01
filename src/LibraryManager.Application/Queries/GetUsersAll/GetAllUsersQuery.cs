using MediatR;
using LibraryManager.Application.Models;

namespace LibraryManager.Application.Queries.GetUsersAll
{
    public class GetAllUsersQuery : IRequest<BaseResult<List<GetAllUsersViewModel>>>
    {
        public GetAllUsersQuery() { }
    }
}
