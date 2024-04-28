using LibraryManager.Application.Models;
using MediatR;

namespace LibraryManager.Application.Queries.GetUsersAll
{
    public class GetAllUsersQuery : IRequest<BaseResult<List<GetAllUsersViewModel>>>
    {
        public GetAllUsersQuery() { }
    }
}
