using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.Infra;

namespace PhoneBook.Lib.App;

public class MigrationAppService : IMigrationAppService
{
    private readonly IMigrationService _migrationService;

    public MigrationAppService(IMigrationService migrationService)
    {
        _migrationService = migrationService;
    }
    
    public async Task MigrateAsync()
    {
        await _migrationService.MigrateAsync();
    }

    public async Task<GetPendingMigrationsResponse> GetPendingMigrationsAsync()
    {
        var pendingMigrations = await _migrationService.GetPendingMigrationsAsync();

        var response = new GetPendingMigrationsResponse(pendingMigrations);

        return response;
    }

    public async Task UpDebugAsync()
    {
        await _migrationService.UpDebugAsync();
    }
}