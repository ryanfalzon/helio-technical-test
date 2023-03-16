using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.App.Factories;

public static class CompanyFactory
{
    public static Company Create(AddCompanyRequest request)
    {
        var company = new Company(request.Name, request.RegistrationDate);
        return company;
    }
}