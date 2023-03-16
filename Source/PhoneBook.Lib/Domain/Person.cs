using PhoneBook.Lib.Infra;

namespace PhoneBook.Lib.Domain;

public class Person : BaseEntity
{
    public string Name { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public string Address { get; protected set; }

    /// <summary>
    /// BI-directional link
    /// </summary>
    public Company Company { get; protected set; }

    protected Person()
    {
    }

    public Person(string name, string phoneNumber, string address)
    {
        Name        = name;
        PhoneNumber = phoneNumber;
        Address     = address;
    }

    public void Update(string name, string phoneNumber, string address)
    {
        Name        = name;
        PhoneNumber = phoneNumber;
        Address     = address;
    }
}