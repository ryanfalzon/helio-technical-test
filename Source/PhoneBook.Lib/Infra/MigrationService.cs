using Microsoft.EntityFrameworkCore;

namespace PhoneBook.Lib.Infra;

public abstract class MigrationService<T> : IMigrationService where T : DbContext
{
    private readonly T _dbContext;

    protected MigrationService(T dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task MigrateAsync()
    {
        await RunMigrations();
    }

    public async Task<string[]> GetPendingMigrationsAsync()
    {
        var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

        var response = pendingMigrations.ToArray();

        return response;
    }

    public async Task UpDebugAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
    }

    private async Task RunMigrations()
    {
        var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

        if (!pendingMigrations.Any())
        {
            return;
        }

        await _dbContext.Database.MigrateAsync();
    }
}