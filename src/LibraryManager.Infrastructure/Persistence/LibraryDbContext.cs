using LibraryManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

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
        }
    }
}
