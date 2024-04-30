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

        public User(string name, string cpf, string password, string email, string role)
            : this()
        {
            Name = name;
            CPF = cpf;
            Password = password;
            Email = email;
            
            if (Enum.TryParse<UserRole>(role, out UserRole parsedRole))
                Role = parsedRole;
            else
                Role = UserRole.Basic;
        }

        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; private set; }
        public UserRole Role { get; set; }
        public LocationInfo? Location { get; private set; }

        public void SetLocation(LocationInfo location)
        {
            Location = location;
        }

        public void Update(string name,
            string cpf,
            string password,
            string email,
            UserRole role,
            LocationInfo location)
        {
            Name = name;
            CPF = cpf;
            Password = password;
            Email = email;
            Role = role;
            Location = location;
        }
    }
}

