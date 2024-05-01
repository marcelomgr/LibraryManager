using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LibraryManager.Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.Name).IsRequired();
                e.Property(u => u.CPF).IsRequired();
                e.Property(u => u.Password).IsRequired();
                e.Property(u => u.Email).IsRequired();
                e.OwnsOne(u => u.Location,
                    builder =>
                    {
                        builder.Property(p => p.Cep).HasColumnName("Cep");
                        builder.Property(p => p.Address).HasColumnName("Address");
                        builder.Property(p => p.District).HasColumnName("District");
                        builder.Property(p => p.City).HasColumnName("City");
                        builder.Property(p => p.State).HasColumnName("State");
                    });
            });

            builder.Entity<Book>(e =>
            {
                e.HasKey(b => b.Id);
                e.Property(b => b.Title).IsRequired();
                e.Property(b => b.Author).IsRequired();
                e.Property(b => b.ISBN).IsRequired();
                e.Property(b => b.PublicationYear).IsRequired();
                e.Property(b => b.Status).IsRequired();
            });

            builder.Entity<Loan>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.ReturnDate);

                entity.HasOne(l => l.User)
                    .WithMany(u => u.Loans)
                    .HasForeignKey(l => l.UserId)
                    .IsRequired();

                entity.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId)
                    .IsRequired();
            });
        }
    }
}
