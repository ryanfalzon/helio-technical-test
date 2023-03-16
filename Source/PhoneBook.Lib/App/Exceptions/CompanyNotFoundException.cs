using PhoneBook.Lib.Domain.Exceptions;

namespace PhoneBook.Lib.App.Exceptions;

public class CompanyNotFoundException : CustomException
{
    public CompanyNotFoundException(int id) : base($"Company [{id}] not found.",
        problemCode: (int) Domain.Exceptions.ProblemCode.CompanyNotFound)
    {
    }
}