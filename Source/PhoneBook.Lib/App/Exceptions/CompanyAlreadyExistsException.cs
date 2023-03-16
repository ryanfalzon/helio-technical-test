using PhoneBook.Lib.Domain.Exceptions;

namespace PhoneBook.Lib.App.Exceptions;

public class CompanyAlreadyExistsException : CustomException
{
    public CompanyAlreadyExistsException(string name) : base($"Company '{name}' already exists.",
        problemCode: (int) Domain.Exceptions.ProblemCode.CompanyAlreadyExists)
    {
    }
    
    public CompanyAlreadyExistsException(string name, Exception innerException) : base($"Company '{name}' already exists.",
        problemCode: (int) Domain.Exceptions.ProblemCode.CompanyAlreadyExists, innerException)
    {
    }
}