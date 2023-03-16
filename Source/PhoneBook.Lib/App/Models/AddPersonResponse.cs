namespace PhoneBook.Lib.App.Models;

public class AddPersonResponse
{
    public int Id { get; }

    public AddPersonResponse(int id)
    {
        Id = id;
    }
}