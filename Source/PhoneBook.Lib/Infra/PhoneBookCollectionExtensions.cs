using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Lib.App;
using PhoneBook.Lib.App.Validation;

namespace PhoneBook.Lib.Infra;

public static class PhoneBookCollectionExtensions
{
    public static void AddPhoneBookConfig(this IServiceCollection serviceCollection, ConnectionStrings connectionStrings)
    {
        // App Services
        serviceCollection.AddScoped<ICompanyAppService, CompanyAppService>();
        serviceCollection.AddScoped<IPersonAppService, PersonAppService>();
        serviceCollection.AddScoped<IMigrationAppService, MigrationAppService>();
        serviceCollection.AddSingleton<CompanyValidationService>();
        serviceCollection.AddSingleton<PersonValidationService>();

        // Infra Services
        serviceCollection.AddDbContext<PhoneBookDbContext>(options => options.UseSqlServer(connectionStrings.PhoneBookDbContext));
        serviceCollection.AddScoped<IMigrationService, PhoneBookMigrationService>();
        serviceCollection.AddTransient<ICompanyRepository, CompanyRepository>();
        serviceCollection.AddTransient<IPersonRepository, PersonRepository>();
    }
}