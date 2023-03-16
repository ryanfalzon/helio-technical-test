using Microsoft.EntityFrameworkCore;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.Infra;

public class PhoneBookDbContext : DbContext
{
    public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PhoneBook");

        modelBuilder.Entity<Company>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Company>()
            .HasIndex(x => x.Name)
            .IsUnique();
        
        modelBuilder.Entity<Company>()
            .HasMany(x => x.People)
            .WithOne(x => x.Company);
        
        modelBuilder.Entity<Person>()
            .HasKey(x => x.Id);
    }
}