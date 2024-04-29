using LibraryManager.Core.Enums;
using LibraryManager.Core.ValueObjects;

namespace LibraryManager.Core.Entities
{
    public class User : BaseEntity
    {
        public User() : base()
        {
            Status = UserStatus.Active;
        }

        public User(string firstName, string fullName, string email, string cpf)
            : this()
        {
            FirstName = firstName;
            FullName = fullName;
            CPF = cpf;
            Email = email;

        }

        public string FirstName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public UserStatus Status { get; private set; }
        public LocationInfo? Location { get; private set; }

        public void SetLocation(LocationInfo location)
        {
            Location = location;
        }

        public void Update(string firstName,
            string fullName,
            string email,
            string cpf,
            LocationInfo location)
        {
            FirstName = firstName;
            FullName = fullName;
            Email = email;
            CPF = cpf;
            Location = location;
        }
    }
}

