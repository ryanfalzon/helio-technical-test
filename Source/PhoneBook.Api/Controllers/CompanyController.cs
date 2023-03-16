using Microsoft.AspNetCore.Mvc;
using PhoneBook.Lib.App;
using PhoneBook.Lib.App.Models;

namespace PhoneBook.Api.Controllers;

/// <inheritdoc />
[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyAppService _companyAppService;

    /// <inheritdoc />
    public CompanyController(ICompanyAppService companyAppService)
    {
        _companyAppService = companyAppService;
    }

    /// <summary>
    /// Retrieves all companies stored in the database
    /// </summary>
    /// <param name="search">Free-text search across all columns</param>
    /// <returns>A list of company DTO objects</returns>
    [HttpGet]
    public async Task<IEnumerable<CompanyDto>> Get([FromQuery] string? search)
    {
        var response = await _companyAppService.Get(search);
        return response;
    }

    /// <summary>
    /// Retrieve a single company
    /// </summary>
    /// <param name="id">Identifier of company to retrieve</param>
    /// <returns>A single company DTO object</returns>
    [HttpGet("{id:int}")]
    public async Task<CompanyDto> GetById(int id)
    {
        var response = await _companyAppService.GetById(id);
        return response;
    }

    /// <summary>
    /// Add a new company to the database
    /// </summary>
    /// <param name="request">The request containing the necessary information to construct a new company</param>
    /// <returns>An object containing the identifier of the newly created company</returns>
    [HttpPost]
    public async Task<AddCompanyResponse> Post(AddCompanyRequest request)
    {
        using (var scope = TransactionScopeFactory.CreateReadCommittedAsync())
        {
            var response = await _companyAppService.Add(request);

            scope.Complete();

            return response;
        }
    }

    /// <summary>
    /// Update an already existing company
    /// </summary>
    /// <param name="request">The request containing the necessary information to update an existing company</param>
    [HttpPut]
    public async Task Put(UpdateCompanyRequest request)
    {
        using (var scope = TransactionScopeFactory.CreateReadCommittedAsync())
        {
            await _companyAppService.Update(request);

            scope.Complete();
        }
    }

    /// <summary>
    /// Delete an already existing company
    /// </summary>
    /// <param name="id">Identifier of company to delete</param>
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        using (var scope = TransactionScopeFactory.CreateReadCommittedAsync())
        {
            await _companyAppService.Delete(id);

            scope.Complete();
        }
    }
}