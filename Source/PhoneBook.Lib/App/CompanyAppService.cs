using System.Linq.Expressions;
using PhoneBook.Lib.App.Exceptions;
using PhoneBook.Lib.App.Factories;
using PhoneBook.Lib.App.Mappers;
using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.App.Validation;
using PhoneBook.Lib.Domain;
using PhoneBook.Lib.Infra;

namespace PhoneBook.Lib.App;

public class CompanyAppService : ICompanyAppService
{
    private readonly PhoneBookDbContext _phoneBookDbContext;
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyValidationService _validationService;

    public CompanyAppService(PhoneBookDbContext phoneBookDbContext, ICompanyRepository companyRepository, CompanyValidationService validationService)
    {
        _phoneBookDbContext     = phoneBookDbContext;
        _companyRepository      = companyRepository;
        _validationService = validationService;
    }

    public async Task<AddCompanyResponse> Add(AddCompanyRequest request)
    {
        _validationService.EnsureIsValid(request);
        
        var company = CompanyFactory.Create(request);

        await _companyRepository.InsertAsync(company);

        try
        {
            await _phoneBookDbContext.SaveChangesAsync();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
        {
            throw new CompanyAlreadyExistsException(request.Name, e);
        }

        return new AddCompanyResponse(company.Id);
    }

    public async Task<IEnumerable<CompanyDto>> Get(string? search = null)
    {
        Expression<Func<Company, bool>>? predicate = null;
        if (!string.IsNullOrWhiteSpace(search))
        {
            predicate = x => x.Name.Contains(search);
        }

        var companies = await _companyRepository.GetWithTotalPeopleAsync(filter: predicate);
        return CompanyDtoMapper.Map(companies);
    }

    public async Task<CompanyDto> GetById(int id)
    {
        var company = await _companyRepository.GetByIdWithTotalPeopleAsync(id);

        if (company == null)
        {
            throw new CompanyNotFoundException(id);
        }
        
        return CompanyDtoMapper.Map(company);
    }

    public async Task Update(UpdateCompanyRequest request)
    {
        _validationService.EnsureIsValid(request);
        
        var company = await _companyRepository.GetByIdAsync(request.Id);

        if (company == null)
        {
            throw new CompanyNotFoundException(request.Id);
        }
        
        company.Update(request.Name);
        await _phoneBookDbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        _companyRepository.Delete(id);
        await _phoneBookDbContext.SaveChangesAsync();
    }
}