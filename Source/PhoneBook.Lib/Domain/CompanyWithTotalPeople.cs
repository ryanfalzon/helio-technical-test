namespace PhoneBook.Lib.Domain;

public class CompanyWithTotalPeople
{
    public int Id { get; set; }
    public string Name { get; }
    public DateTimeOffset RegistrationDate { get; }
    public int TotalPeople { get; }

    public CompanyWithTotalPeople(int id, string name, DateTimeOffset registrationDate, int totalPeople)
    {
        Id               = id;
        Name             = name;
        RegistrationDate = registrationDate;
        TotalPeople      = totalPeople;
    }
}