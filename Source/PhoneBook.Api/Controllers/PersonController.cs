using Microsoft.AspNetCore.Mvc;
using PhoneBook.Lib.App;
using PhoneBook.Lib.App.Models;

namespace PhoneBook.Api.Controllers;

/// <inheritdoc />
[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonAppService _personAppService;

    /// <inheritdoc />
    public PersonController(IPersonAppService personAppService)
    {
        _personAppService = personAppService;
    }

    /// <summary>
    /// Retrieves all people stored in the database
    /// </summary>
    /// <param name="search">Free-text search across all columns</param>
    /// <returns>A list of people DTO objects</returns>
    [HttpGet]
    public async Task<IEnumerable<PersonDto>> Get([FromQuery] string? search)
    {
        var response = await _personAppService.Get(search);
        return response;
    }

    /// <summary>
    /// Retrieve a single person
    /// </summary>
    /// <param name="id">Identifier of person to retrieve</param>
    /// <returns>A single person DTO object</returns>
    [HttpGet("{id:int}")]
    public async Task<PersonDto> GetById(int id)
    {
        var response = await _personAppService.GetById(id);
        return response;
    }

    /// <summary>
    /// Retrieve a random person
    /// </summary>
    /// <returns>A single person DTO object</returns>
    [HttpGet("Random")]
    public async Task<PersonDto> GetById()
    {
        var response = await _personAppService.GetRandom();
        return response;
    }

    /// <summary>
    /// Add a new person to the database
    /// </summary>
    /// <param name="request">The request containing the necessary information to construct a new person</param>
    /// <returns>An object containing the identifier of the new created person</returns>
    [HttpPost]
    public async Task<AddPersonResponse> Post(AddPersonRequest request)
    {
        using (var scope = TransactionScopeFactory.CreateReadCommittedAsync())
        {
            var response = await _personAppService.Add(request);
            
            scope.Complete();

            return response;
        }
    }

    /// <summary>
    /// Update an already existing person
    /// </summary>
    /// <param name="request">The request containing the necessary information to update an existing person</param>
    [HttpPut]
    public async Task Put(UpdatePersonRequest request)
    {
        using (var scope = TransactionScopeFactory.CreateReadCommittedAsync())
        {
            await _personAppService.Update(request);
            
            scope.Complete();
        }
    }

    /// <summary>
    /// Delete an already existing person
    /// </summary>
    /// <param name="id">Identifier of person to delete</param>
    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        using (var scope = TransactionScopeFactory.CreateReadCommittedAsync())
        {
            await _personAppService.Delete(id);
            
            scope.Complete();
        }
    }
}