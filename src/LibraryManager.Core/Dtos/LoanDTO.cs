
namespace LibraryManager.Core.Dtos
{
    public class LoanDTO
    {
        public Guid Id { get; set; }
        public UserDTO User { get; set; }
        public BookDTO Book { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
