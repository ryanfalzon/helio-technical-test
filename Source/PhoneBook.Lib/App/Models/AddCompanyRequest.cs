namespace PhoneBook.Lib.App.Models;

public class AddCompanyRequest
{
    public string? Name { get; set; }
    public DateTimeOffset RegistrationDate { get; set; }
}