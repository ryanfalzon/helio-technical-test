namespace PhoneBook.Lib.Domain.Exceptions;

public enum ProblemCode
{
    CompanyNotFound = 100_001,
    CompanyAlreadyExists = 100_002,
    PersonNotFound = 100_003,
    RandomPersonCouldNotBeLoaded = 100_004,
    InvalidRequest = 100_005
}