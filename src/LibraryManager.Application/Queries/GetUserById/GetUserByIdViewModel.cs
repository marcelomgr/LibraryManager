using LibraryManager.Core.Entities;

namespace LibraryManager.Application.Queries.GetUserById
{
    public class GetUserByIdViewModel
    {
        public GetUserByIdViewModel(User user)
        {
            FirstName = user.FirstName;
            FullName = user.FullName;
            Email = user.Email;
            CPF = user.CPF;

            //Country = user.Location?.Country;
            //Website = user.Contact?.Website;
        }

        public string FirstName { get; private set; }
        public string FullName { get; private set; }
        public string? Email { get; private set; }
        public string? CPF { get; private set; }
    }
}