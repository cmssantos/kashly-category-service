using Cms.AspNetCore.JsonLocalizer.Interfaces;
using FluentValidation;
using Kashly.Category.Communication.Requests;

namespace Kashly.Category.Application.UseCases;

public class CategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CategoryRequestValidator(ILocalizer localizer)
    {
        RuleFor(category => category.Description)
            .NotEmpty()
            .WithMessage(localizer.GetString("validation.required", "Description").Value);
    }
}
