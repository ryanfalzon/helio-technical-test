namespace PhoneBook.Lib.App.Models;

public class CompanyDto
{
    public int Id { get; }
    public string Name { get; }
    public DateTimeOffset RegistrationDate { get; }
    public int TotalPeopleCount { get; }

    public CompanyDto(int id, string name, DateTimeOffset registrationDate, int totalPeopleCount)
    {
        Id               = id;
        Name             = name;
        RegistrationDate = registrationDate;
        TotalPeopleCount = totalPeopleCount;
    }
}