using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.App.Mappers;

public static class CompanyDtoMapper
{
    public static IEnumerable<CompanyDto> Map(IEnumerable<CompanyWithTotalPeople> companies)
    {
        var mappedCompanies = new List<CompanyDto>();

        foreach (var company in companies)
        {
            var mappedCompany = Map(company);
            mappedCompanies.Add(mappedCompany);
        }

        return mappedCompanies;
    }
    
    public static CompanyDto Map(CompanyWithTotalPeople company)
    {
        var mappedCompany = new CompanyDto(company.Id, company.Name, company.RegistrationDate, company.TotalPeople);
        return mappedCompany;
    }
}