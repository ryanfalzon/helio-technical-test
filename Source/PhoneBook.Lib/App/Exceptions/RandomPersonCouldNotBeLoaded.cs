using PhoneBook.Lib.Domain.Exceptions;

namespace PhoneBook.Lib.App.Exceptions;

public class RandomPersonCouldNotBeLoaded : CustomException
{
    public RandomPersonCouldNotBeLoaded() : base("A random person could not be loaded due to an error.",
        problemCode: (int) Domain.Exceptions.ProblemCode.RandomPersonCouldNotBeLoaded)
    {
    }
}