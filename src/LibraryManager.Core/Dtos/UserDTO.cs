using LibraryManager.Core.Enums;

namespace LibraryManager.Core.Dtos
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
        public LocationInfoDTO Location { get; set; }
    }
}
