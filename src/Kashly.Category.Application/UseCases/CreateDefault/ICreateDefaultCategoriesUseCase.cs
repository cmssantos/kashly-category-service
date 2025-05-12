namespace Kashly.Category.Application.UseCases.CreateDefault;

public interface ICreateDefaultCategoriesUseCase
{
    Task Handle(CancellationToken cancellationToken);
}
