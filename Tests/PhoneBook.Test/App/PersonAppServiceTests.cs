using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PhoneBook.Lib.App;
using PhoneBook.Lib.App.Exceptions;
using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.App.Validation;
using PhoneBook.Lib.Infra;
using PhoneBook.Test.Infra;

namespace PhoneBook.Test.App;

[TestFixture]
public class PersonAppServiceTests
{
    [Test]
    public async Task Person_Add()
    {
        using var mockDatabase = new MockDatabase(nameof(Person_Add));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);
        var personRepository = new PersonRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();
        var personValidationServiceMock = new Mock<PersonValidationService>();

        var companyAppService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);
        var personAppService = new PersonAppService(mockDbContext, companyRepository, personRepository, personValidationServiceMock.Object);

        var addPersonResponse = await AddPerson(companyAppService, personAppService, "PersonA");

        var response = await personAppService.GetById(addPersonResponse.Id);

        Assert.That(response.FullName, Is.EqualTo("PersonA"));
    }

    [Test]
    public async Task Person_Edit()
    {
        using var mockDatabase = new MockDatabase(nameof(Person_Edit));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);
        var personRepository = new PersonRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();
        var personValidationServiceMock = new Mock<PersonValidationService>();

        var companyAppService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);
        var personAppService = new PersonAppService(mockDbContext, companyRepository, personRepository, personValidationServiceMock.Object);

        var addPersonResponse = await AddPerson(companyAppService, personAppService);
        await personAppService.Update(new UpdatePersonRequest
        {
            Id          = addPersonResponse.Id,
            FullName    = "PersonA",
            PhoneNumber = "21249200",
            Address     = "Address Changed"
        });

        var response = await personAppService.GetById(addPersonResponse.Id);

        Assert.That(response.Address, Is.EqualTo("Address Changed"));
    }

    [Test]
    public async Task Person_Delete()
    {
        using var mockDatabase = new MockDatabase(nameof(Person_Delete));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);
        var personRepository = new PersonRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();
        var personValidationServiceMock = new Mock<PersonValidationService>();

        var companyAppService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);
        var personAppService = new PersonAppService(mockDbContext, companyRepository, personRepository, personValidationServiceMock.Object);

        var addPersonResponse = await AddPerson(companyAppService, personAppService);

        await personAppService.Delete(addPersonResponse.Id);

        Assert.ThrowsAsync<PersonNotFoundException>(() => personAppService.GetById(addPersonResponse.Id));
    }

    [Test]
    public async Task Person_GetAll()
    {
        using var mockDatabase = new MockDatabase(nameof(Person_GetAll));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);
        var personRepository = new PersonRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();
        var personValidationServiceMock = new Mock<PersonValidationService>();

        var companyAppService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);
        var personAppService = new PersonAppService(mockDbContext, companyRepository, personRepository, personValidationServiceMock.Object);

        await AddPerson(companyAppService, personAppService);
        await AddPerson(companyAppService, personAppService);

        var response = await personAppService.Get();

        Assert.That(response.Count(), Is.EqualTo(2));
    }

    [TestCase("PersonA", 0)]
    [TestCase("Some Address For B", 1)]
    [TestCase("21249200", 2)]
    [TestCase("CompanyA", 1)]
    public async Task Person_Search(string searchCriteria, int expectedCount)
    {
        using var mockDatabase = new MockDatabase(nameof(Person_Search));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);
        var personRepository = new PersonRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();
        var personValidationServiceMock = new Mock<PersonValidationService>();

        var companyAppService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);
        var personAppService = new PersonAppService(mockDbContext, companyRepository, personRepository, personValidationServiceMock.Object);

        await AddPerson(companyAppService, personAppService, "PersonB", "21249200", "Some Address For A", "CompanyA");
        await AddPerson(companyAppService, personAppService, "PersonC", "21249200", "Some Address For B", "CompanyB");

        var response = await personAppService.Get(searchCriteria);

        Assert.That(response.Count(), Is.EqualTo(expectedCount));
    }

    [Test]
    public async Task Person_WildCard()
    {
        using var mockDatabase = new MockDatabase(nameof(Person_GetAll));

        var mockDbContext = await mockDatabase.CreateMockDbContextAsync();
        var companyRepository = new CompanyRepository(mockDbContext);
        var personRepository = new PersonRepository(mockDbContext);

        var companyValidationServiceMock = new Mock<CompanyValidationService>();
        var personValidationServiceMock = new Mock<PersonValidationService>();

        var companyAppService = new CompanyAppService(mockDbContext, companyRepository, companyValidationServiceMock.Object);
        var personAppService = new PersonAppService(mockDbContext, companyRepository, personRepository, personValidationServiceMock.Object);

        await AddPerson(companyAppService, personAppService);
        await AddPerson(companyAppService, personAppService);
        await AddPerson(companyAppService, personAppService);
        await AddPerson(companyAppService, personAppService);
        await AddPerson(companyAppService, personAppService);
        await AddPerson(companyAppService, personAppService);

        Assert.DoesNotThrowAsync(() => personAppService.GetRandom());
    }

    private static async Task<AddPersonResponse> AddPerson(ICompanyAppService companyAppService, IPersonAppService personAppService, string? name = null,
        string? phoneNumber = null, string? address = null, string? companyName = null)
    {
        int companyId;
        
        var companies = await companyAppService.Get(companyName);
        var companyDtos = companies.ToList();
        if (companyDtos.Any())
        {
            companyId = companyDtos.Single().Id;
        }
        else
        {
            var addCompanyResponse = await companyAppService.Add(new AddCompanyRequest
            {
                Name             = companyName ?? RandomString(10),
                RegistrationDate = DateTimeOffset.Now
            });

            companyId = addCompanyResponse.Id;
        }

        return await personAppService.Add(new AddPersonRequest
        {
            FullName    = name ?? RandomString(10),
            PhoneNumber = phoneNumber ?? RandomString(10),
            Address     = address ?? RandomString(10),
            CompanyId   = companyId
        });
    }
    
    private static string RandomString(int length)
    {
        const string charBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        
        var rand = new Random();
        return new string(Enumerable.Range(0,length)
            .Select(_ => charBase[rand.Next(charBase.Length)])
            .ToArray());
    }
}