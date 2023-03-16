using PhoneBook.Lib.App.Models;

namespace PhoneBook.Lib.App;

public interface IPersonAppService
{
    Task<AddPersonResponse> Add(AddPersonRequest request);
    Task<IEnumerable<PersonDto>> Get(string? search);
    Task<PersonDto> GetById(int id);
    Task<PersonDto> GetRandom();
    Task Update(UpdatePersonRequest request);
    Task Delete(int id);
}