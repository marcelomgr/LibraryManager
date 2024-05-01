using System.Text;
using LibraryManager.Core.Enums;
using System.Security.Cryptography;
using LibraryManager.Core.ValueObjects;

namespace LibraryManager.Core.Entities
{
    public class User : BaseEntity
    {   
        private User() { }

        public User(string name, string cpf, string password, string email, string role) : base()
        {
            Name = name;
            CPF = NormalizeCPF(cpf);
            Password = HashPassword(password);
            Email = email;
            Status = UserStatus.Active;
            Loans = new List<Loan>();

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
        public List<Loan> Loans { get; private set; }

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

        public void SetLocation(LocationInfo location)
        {
            Location = location;
        }

        public static string NormalizeCPF(string cpf)
        {
            return cpf.Trim().Replace(".", "").Replace("-", "");
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();

                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
