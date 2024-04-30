using LibraryManager.Core.Entities;
using LibraryManager.Application.Commands.UpdateUser;

namespace LibraryManager.Application.Queries.GetUserByCpf
{
    public class GetUserByCpfViewModel
    {
        public GetUserByCpfViewModel(User user)
        {
            FirstName = user.FirstName;
            FullName = user.FullName;
            Email = user.Email;
            CPF = user.CPF;
            Location = user.Location != null ? new LocationInfoModel(user.Location) : null;
        }

        public string FirstName { get; private set; }
        public string FullName { get; private set; }
        public string? Email { get; private set; }
        public string? CPF { get; private set; }
        public LocationInfoModel? Location { get; set; }
    }
}
