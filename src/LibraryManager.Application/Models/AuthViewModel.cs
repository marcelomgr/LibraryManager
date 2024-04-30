
namespace LibraryManager.Application.Models
{
    public class AuthViewModel
    {
        public AuthViewModel(string name, string cpf, string token)
        {
            Name = name;
            CPF = cpf;
            Token = token;
        }

        public string Name { get; private set; }
        public string CPF { get; private set; }
        public string Token { get; private set; }
    }
}
