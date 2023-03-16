namespace PhoneBook.Lib.App.Models;

public class PersonDto
{
    public int Id { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }
    public string Address { get; }
    public string CompanyName { get; }

    public PersonDto(int id, string fullName, string phoneNumber, string address, string companyName)
    {
        Id          = id;
        FullName    = fullName;
        PhoneNumber = phoneNumber;
        Address     = address;
        CompanyName = companyName;
    }
}