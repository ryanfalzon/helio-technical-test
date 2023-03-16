using PhoneBook.Lib.App.Models;
using PhoneBook.Lib.Domain;

namespace PhoneBook.Lib.App.Mappers;

public class PersonDtoMapper
{
    public static IEnumerable<PersonDto> Map(IEnumerable<Person> people)
    {
        var mappedPeople = new List<PersonDto>();

        foreach (var person in people)
        {
            var mappedPerson = Map(person);
            mappedPeople.Add(mappedPerson);
        }

        return mappedPeople;
    }
    
    public static PersonDto Map(Person person)
    {
        var mappedPerson = new PersonDto(person.Id, person.Name, person.PhoneNumber, person.Address, person.Company.Name);
        return mappedPerson;
    }
}