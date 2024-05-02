using LibraryManager.Core.Enums;

namespace LibraryManager.Core.Dtos
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public BookStatus Status { get; set; }
    }
}
