using Microsoft.AspNetCore.Mvc;
using PhoneBook.Lib.App;
using PhoneBook.Lib.App.Models;

namespace PhoneBook.Api.Controllers;

/// <inheritdoc />
[ApiController]
[ApiVersion("1.0")]
[Route("Migration")]
public class MigrationController : ControllerBase
{
    private readonly IMigrationAppService _migrationAppService;

    /// <inheritdoc />
    public MigrationController(IMigrationAppService migrationAppService)
    {
        _migrationAppService = migrationAppService;
    }

    /// <summary>
    /// Applies all pending migrations
    /// </summary>
    [HttpPatch]
    public async Task Migrate()
    {
        await _migrationAppService.MigrateAsync();
    }

    /// <summary>
    /// Gets all pending migrations
    /// </summary>
    /// <returns>A list of pending migration names</returns>
    [HttpGet]
    public async Task<GetPendingMigrationsResponse> GetPendingMigrations()
    {
        var response = await _migrationAppService.GetPendingMigrationsAsync();
        return response;
    }
}