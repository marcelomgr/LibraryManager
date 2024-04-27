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

        //public User(string firstName, string fullName, string email, string cpf)
        //    : base()
        //{
        //    FirstName = firstName;
        //    FullName = fullName;
        //    CPF = cpf;
        //    Email = email;

        //    Status = UserStatus.Active;
        //}

        public string FirstName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public UserStatus Status { get; private set; }
        //public LocationInfo? Location { get; private set; }
        //public ContactInfo? Contact { get; private set; }

        public void Update(string firstName,
            string fullName,
            string email,
            string cpf
            //LocationInfo location,
            //ContactInfo contact
            )
        {
            FirstName = firstName;
            FullName = fullName;
            Email = email;
            CPF = cpf;
            //Location = location;
            //Contact = contact;
        }
    }
}

