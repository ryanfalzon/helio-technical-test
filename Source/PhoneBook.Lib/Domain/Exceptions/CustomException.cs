namespace PhoneBook.Lib.Domain.Exceptions;

public class CustomException : Exception
{
    public readonly int ProblemCode;

    protected CustomException(string message, int problemCode) : base(message)
    {
        ProblemCode = problemCode;
    }

    protected CustomException(string message, int problemCode, Exception innerException) : base(message, innerException)
    {
        ProblemCode = problemCode;
    }
}