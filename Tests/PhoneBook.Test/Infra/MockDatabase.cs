using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Lib.Infra;

namespace PhoneBook.Test.Infra;

public class MockDatabase : IDisposable
{
    private readonly MockDbContext _mockDbContext;

    public MockDatabase(string filename)
    {
        var options = new DbContextOptionsBuilder<PhoneBookDbContext>()
            .UseSqlite(connectionString: $"Filename={filename}.db")
            .Options;

        _mockDbContext = new MockDbContext(options);
    }

    public async Task<MockDbContext> CreateMockDbContextAsync()
    {
        await _mockDbContext.Database.EnsureCreatedAsync();

        return _mockDbContext;
    }

    public void Dispose()
    {
        _mockDbContext.Database.EnsureDeleted();
    }
}