using PhoneBook.Lib.App.Models;

namespace PhoneBook.Lib.App;

public interface ICompanyAppService
{
    Task<AddCompanyResponse> Add(AddCompanyRequest request);
    Task<IEnumerable<CompanyDto>> Get(string? search);
    Task<CompanyDto> GetById(int id);
    Task Update(UpdateCompanyRequest request);
    Task Delete(int id);
}