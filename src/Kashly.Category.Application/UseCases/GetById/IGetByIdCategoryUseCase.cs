using Kashly.Category.Communication.Requests;
using Kashly.Category.Communication.Responses;

namespace Kashly.Category.Application.UseCases.GetById;

public interface IGetByIdCategoryUseCase
{
    Task<CategoryResponse> Handle(GetByIdCategoryRequest request, CancellationToken cancellationToken);
}
