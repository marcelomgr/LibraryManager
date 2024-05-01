using LibraryManager.Core.Enums;

namespace LibraryManager.Core.Entities
{
    public class Book : BaseEntity
    {
        public Book(string title, string author, string iSBN, int publicationYear)
        {
            Title = title;
            Author = author;
            ISBN = iSBN;
            PublicationYear = publicationYear;
            Status = BookStatus.Available;
            Loans = new List<Loan>();
        }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public int PublicationYear { get; private set; }
        public BookStatus Status { get; private set; }
        public List<Loan> Loans { get; private set; }

        public void Update(string title,
            string author,
            string isbn,
            int publicationYear)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
        }
    }
}
