using FluentValidation;

namespace Kashly.Category.Application.Interfaces;

public interface IValidatorService
{
    void Validate<T>(T request, IValidator<T> validator);
}
