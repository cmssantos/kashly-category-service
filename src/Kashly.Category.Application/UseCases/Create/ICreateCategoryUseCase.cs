using Kashly.Category.Communication.Requests;

namespace Kashly.Category.Application.UseCases.Create;

public interface ICreateCategoryUseCase
{
    Task<int> Handle(CreateCategoryRequest request, CancellationToken cancellationToken);
}
