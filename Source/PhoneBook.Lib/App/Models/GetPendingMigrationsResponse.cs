namespace PhoneBook.Lib.App.Models;

public class GetPendingMigrationsResponse
{
    public string[] PendingMigrations { get; }

    public GetPendingMigrationsResponse(string[] pendingMigrations)
    {
        PendingMigrations = pendingMigrations;
    }
}