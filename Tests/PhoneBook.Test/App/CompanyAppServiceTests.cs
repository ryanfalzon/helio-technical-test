using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhoneBook.Lib.App;
using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.App.Validation;
using PhoneBook.Lib.Infra;
using PhoneBook.Test.Infra;

namespace PhoneBook.Test.App;

[TestFixture]
public class CompanyAppServiceTests
{
    [Test]
    public async Task Company_Add()
    {
        using var mockDatabase = new MockDatabase(nameof(Company_Add));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();

        var appService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);

        var addCompanyResponse = await AddCompany(appService, "CompanyA");

        var response = await appService.GetById(addCompanyResponse.Id);

        Assert.That(response.Name, Is.EqualTo("CompanyA"));
    }

    [Test]
    public async Task Company_GetAll()
    {
        using var mockDatabase = new MockDatabase(nameof(Company_GetAll));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();

        var appService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);

        await AddCompany(appService, "CompanyA");
        await AddCompany(appService, "CompanyB");

        var response = await appService.Get();

        Assert.That(response.Count(), Is.EqualTo(2));
    }

    private static async Task<AddCompanyResponse> AddCompany(ICompanyAppService appService, string name, DateTimeOffset? registrationDate = null)
    {
        return await appService.Add(new AddCompanyRequest
        {
            Name             = name,
            RegistrationDate = registrationDate ?? DateTimeOffset.Now
        });
    }
}