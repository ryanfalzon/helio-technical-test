namespace PhoneBook.Lib.Infra;

public class PhoneBookMigrationService : MigrationService<PhoneBookDbContext>
{
    public PhoneBookMigrationService(PhoneBookDbContext dbContext) : base(dbContext)
    {
    }
}