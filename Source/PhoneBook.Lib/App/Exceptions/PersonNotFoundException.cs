using PhoneBook.Lib.Domain.Exceptions;

namespace PhoneBook.Lib.App.Exceptions;

public class PersonNotFoundException : CustomException
{
    public PersonNotFoundException(int id) : base($"Person [{id}] not found.",
        problemCode: (int) Domain.Exceptions.ProblemCode.PersonNotFound)
    {
    }
}