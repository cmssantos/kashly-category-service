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
        CreateMap<Domain.Entities.Category, Communication.Responses.CategoryShortResponse>()
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Domain.Entities.Category, Communication.Responses.CategoryResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}
