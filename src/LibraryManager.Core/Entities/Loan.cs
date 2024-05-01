
namespace LibraryManager.Core.Entities
{
    public class Loan : BaseEntity
    {
        public Loan(Guid userId, Guid bookId)
        {
            UserId = userId;
            BookId = bookId;
            ReturnDate = null;
        }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public DateTime? ReturnDate { get; private set; }

        public void UpdateReturnDate()
        {
            ReturnDate = DateTime.UtcNow;
        }
    }
}
