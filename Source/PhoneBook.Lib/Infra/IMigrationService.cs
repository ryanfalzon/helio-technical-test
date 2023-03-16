namespace PhoneBook.Lib.Infra;

public interface IMigrationService
{
    Task MigrateAsync();

    Task<string[]> GetPendingMigrationsAsync();

    Task UpDebugAsync();
}