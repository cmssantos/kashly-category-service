using AutoMapper;
using Kashly.Category.Communication.Requests;

namespace Kashly.Category.Application.AutoMapper;

internal class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<CreateCategoryRequest, Domain.Entities.Category>()
             .ConstructUsing(src => new Domain.Entities.Category(
                (Domain.Enums.CategoryType)src.Type,
                src.Description,
                src.Icon,
                src.Color,
                "" // Set after UserId with AssignUser
            ));
    }

    private void EntityToResponse()
    {
    }
}
