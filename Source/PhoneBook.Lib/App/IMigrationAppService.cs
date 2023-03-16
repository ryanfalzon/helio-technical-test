using PhoneBook.Lib.App.Models;

namespace PhoneBook.Lib.App;

public interface IMigrationAppService
{
    Task MigrateAsync();
    Task<GetPendingMigrationsResponse> GetPendingMigrationsAsync();
    Task UpDebugAsync();
}