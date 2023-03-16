using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.App.Factories;

public static class PersonFactory
{
    public static Person Create(AddPersonRequest request)
    {
        var person = new Person(request.FullName, request.PhoneNumber, request.Address);
        return person;
    }
}