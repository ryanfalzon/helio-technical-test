using PhoneBook.Lib.App.Exceptions;
using PhoneBook.Lib.App.Models;

namespace PhoneBook.Lib.App.Validation;

public class CompanyValidationService
{
    public void EnsureIsValid(AddCompanyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.Name)}' for [AddCompanyRequest]",
                nameof(request.Name));
        }

        if (request.RegistrationDate > DateTimeOffset.Now)
        {
            throw new InvalidRequestException("Registration date cannot be in the futures for [AddCompanyRequest]",
                nameof(request.RegistrationDate));
        }
    }

    public void EnsureIsValid(UpdateCompanyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.Name)}' for [UpdateCompanyRequest]",
                nameof(request.Name));
        }
    }
}