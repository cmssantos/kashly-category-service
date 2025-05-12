using FluentValidation;
using Kashly.Category.Application.Interfaces;
using Kashly.Category.Domain.Exceptions;

namespace Kashly.Category.Application.Services;

public class ValidationService : IValidatorService
{
    public void Validate<T>(T request, IValidator<T> validator)
    {
        FluentValidation.Results.ValidationResult result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
