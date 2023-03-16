using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Lib.App.Exceptions;
using PhoneBook.Lib.App.Factories;
using PhoneBook.Lib.App.Mappers;
using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.App.Validation;
using PhoneBook.Lib.Domain;
using PhoneBook.Lib.Infra;

namespace PhoneBook.Lib.App;

public class PersonAppService : IPersonAppService
{
    private readonly PhoneBookDbContext _phoneBookDbContext;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPersonRepository _personRepository;
    private readonly PersonValidationService _validationService;

    public PersonAppService(PhoneBookDbContext phoneBookDbContext, ICompanyRepository companyRepository, IPersonRepository personRepository,
        PersonValidationService validationService)
    {
        _phoneBookDbContext = phoneBookDbContext;
        _companyRepository  = companyRepository;
        _personRepository   = personRepository;
        _validationService  = validationService;
    }

    public async Task<AddPersonResponse> Add(AddPersonRequest request)
    {
        _validationService.EnsureIsValid(request);
        
        var company = await _companyRepository.GetByIdAsync(request.CompanyId);

        if (company == null)
        {
            throw new CompanyNotFoundException(request.CompanyId);
        }

        var person = PersonFactory.Create(request);
        company.AddPerson(person);

        await _phoneBookDbContext.SaveChangesAsync();

        return new AddPersonResponse(person.Id);
    }

    public async Task<IEnumerable<PersonDto>> Get(string? search = null)
    {
        Expression<Func<Person, bool>>? predicate = null;
        if (!string.IsNullOrWhiteSpace(search))
        {
            predicate = x => x.Name.Contains(search) || x.Address.Contains(search) ||
                x.PhoneNumber.Contains(search) || x.Company.Name.Contains(search);
        }

        var people = await _personRepository.GetAsync(
            filter: predicate,
            include: source => source.Include(x => x.Company));

        return PersonDtoMapper.Map(people);
    }

    public async Task<PersonDto> GetById(int id)
    {
        var person = await _personRepository.GetByIdAsync(id,
            include: source => source.Include(x => x.Company));

        if (person == null)
        {
            throw new PersonNotFoundException(id);
        }

        return PersonDtoMapper.Map(person);
    }

    public async Task<PersonDto> GetRandom()
    {
        var person = await _personRepository.GetRandom(include: source => source.Include(x => x.Company));

        if (person == null)
        {
            throw new RandomPersonCouldNotBeLoaded();
        }

        return PersonDtoMapper.Map(person);
    }

    public async Task Update(UpdatePersonRequest request)
    {
        _validationService.EnsureIsValid(request);
        
        var person = await _personRepository.GetByIdAsync(request.Id);

        if (person == null)
        {
            throw new PersonNotFoundException(request.Id);
        }

        person.Update(request.FullName, request.PhoneNumber, request.Address);
        await _phoneBookDbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _personRepository.Delete(id);
        await _phoneBookDbContext.SaveChangesAsync();
    }
}