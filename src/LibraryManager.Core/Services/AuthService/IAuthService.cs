using LibraryManager.Core.Entities;

namespace LibraryManager.Core.Services.AuthService
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
}
