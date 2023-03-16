using Microsoft.EntityFrameworkCore;
using PhoneBook.Lib.Domain;
using PhoneBook.Lib.Infra;

namespace PhoneBook.Test.Infra;

public class MockDbContext : PhoneBookDbContext
{
    public MockDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Mock");

        modelBuilder.Entity<Company>();
        modelBuilder.Entity<Person>();

        base.OnModelCreating(modelBuilder);
    }
}