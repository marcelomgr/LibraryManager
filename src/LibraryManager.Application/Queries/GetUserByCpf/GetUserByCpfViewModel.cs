using LibraryManager.Core.Entities;
using LibraryManager.Application.Commands.UpdateUser;

namespace LibraryManager.Application.Queries.GetUserByCpf
{
    public class GetUserByCpfViewModel
    {
        public GetUserByCpfViewModel(User user)
        {
            Name = user.Name;
            CPF = user.CPF;
            Email = user.Email;
            Location = user.Location is not null ? new LocationInfoModel(user.Location) : null;
        }

        public string Name { get; private set; }
        public string? CPF { get; private set; }
        public string? Email { get; private set; }
        public LocationInfoModel? Location { get; set; }
    }
}
