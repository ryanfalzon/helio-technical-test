using PhoneBook.Lib.App.Exceptions;
using PhoneBook.Lib.App.Models;

namespace PhoneBook.Lib.App.Validation;

public class PersonValidationService
{
    public void EnsureIsValid(AddPersonRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.FullName)}' for [AddPersonRequest]",
                nameof(request.FullName));
        }
        
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.PhoneNumber)}' for [AddPersonRequest]",
                nameof(request.PhoneNumber));
        }
        
        if (string.IsNullOrWhiteSpace(request.Address))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.Address)}' for [AddPersonRequest]",
                nameof(request.Address));
        }
    }

    public void EnsureIsValid(UpdatePersonRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.FullName)}' for [UpdatePersonRequest]",
                nameof(request.FullName));
        }
        
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.PhoneNumber)}' for [UpdatePersonRequest]",
                nameof(request.PhoneNumber));
        }
        
        if (string.IsNullOrWhiteSpace(request.Address))
        {
            throw new InvalidRequestException($"Invalid value passed for property '{nameof(request.Address)}' for [UpdatePersonRequest]",
                nameof(request.Address));
        }
    }
}