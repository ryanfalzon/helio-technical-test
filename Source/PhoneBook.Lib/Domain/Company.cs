using PhoneBook.Lib.Infra;

namespace PhoneBook.Lib.Domain;

public class Company : BaseEntity
{
    public string Name { get; protected set; }
    public DateTimeOffset RegistrationDate { get; protected set; }
    public ICollection<Person> People { get; protected set; }

    /// <summary>
    /// Required by EF
    /// </summary>
    protected Company()
    {
        People = new List<Person>();
    }

    public Company(string name, DateTimeOffset registrationDate)
    {
        Name             = name;
        RegistrationDate = registrationDate;
        People           = new List<Person>();
    }

    public void Update(string name)
    {
        Name = name;
    }

    public void AddPerson(Person person)
    {
        People.Add(person);
    }
}