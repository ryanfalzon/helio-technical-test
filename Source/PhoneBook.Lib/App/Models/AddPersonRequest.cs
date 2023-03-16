namespace PhoneBook.Lib.App.Models;

public class AddPersonRequest
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public int CompanyId { get; set; }
}