using PhoneBook.Lib.Domain.Exceptions;

namespace PhoneBook.Lib.App.Exceptions;

public class InvalidRequestException : CustomException
{
    public string InvalidProperty { get; }

    public InvalidRequestException(string message, string invalidProperty) : base(message,
        problemCode: (int) Domain.Exceptions.ProblemCode.InvalidRequest)
    {
        InvalidProperty = invalidProperty;
    }
}